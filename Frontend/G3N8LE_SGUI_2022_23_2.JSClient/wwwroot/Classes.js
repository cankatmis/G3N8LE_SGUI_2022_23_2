let classes = [];
let connection = null;
getdata();
setupSignalR();
let classIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:60617/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ClassCreated", (user, message) => {
        getdata();
    });

    connection.on("ClassDeleted", (user, message) => {
        getdata();
    });
    connection.on("ClassUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};
async function getdata() {
    await fetch('http://localhost:60617/Classes')
        .then(x => x.json())
        .then(y => {
            classes = y;
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    classes.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" + t.grading + "</td><td>" + t.price + " $</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update Price</button>`
            + "</td></tr>";

    });
    document.getElementById('classname').value = "";
    document.getElementById('classgrading').value = 1;
    document.getElementById('classcost').value = "";
}
function showupdate(id) {
    document.getElementById('classcostToUpdate').value = classes.find(t => t['id'] == id)['price'];
    document.getElementById('updateformdiv').style.display = 'flex';
    classIdToUpdate = id;
}
function remove(id) {
    fetch('http://localhost:60617/Classes/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function create() {
    let Classname = document.getElementById('classname').value;
    let Classgrading = document.getElementById('classgrading').value;
    let Classcost = document.getElementById('classcost').value;

    fetch('http://localhost:60617/Classes', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Name: Classname, Grading: Classgrading, Price: Classcost })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let ClasscostToUpd = document.getElementById('classcostToUpdate').value;
    let Classname = classes.find(t => t['id'] == classIdToUpdate)['name'];
    let Classgrading = classes.find(t => t['id'] == classIdToUpdate)['grading'];
    fetch('http://localhost:60617/classes', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Name: Classname, Grading: Classgrading, Price: ClasscostToUpd, Id: classIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}