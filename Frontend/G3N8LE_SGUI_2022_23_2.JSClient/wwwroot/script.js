let teachers = [];
let connection = null;
getdata();
setupSignalR();
let teacherIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:60617/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("TeacherCreated", (user, message) => {
        getdata();
    });

    connection.on("TeacherDeleted", (user, message) => {
        getdata();
    });
    connection.on("TeacherUpdated", (user, message) => {
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
    await fetch('http://localhost:60617/teachers')
        .then(x => x.json())
        .then(y => {
            teachers = y;
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    teachers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" + t.branch + "</td><td>" + t.price + " $</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update Cost</button>`
            + "</td></tr>";
    });
    document.getElementById('teachername').value = "";
    document.getElementById('teacherbranch').value = "";
    document.getElementById('teachercost').value = "";
}
function showupdate(id) {
    document.getElementById('teachercostToUpdate').value = teachers.find(t => t['id'] == id)['price'];
    //document.getElementById('teacherbranchToUpdate').value = teachers.find(t => t['id'] == id)['branch'];
    document.getElementById('updateformdiv').style.display = 'flex';
    teacherIdToUpdate = id;


}
function remove(id) {
    fetch('http://localhost:60617/teachers/' + id, {
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
    let Teachername = document.getElementById('teachername').value;
    let Teacherbranch = document.getElementById('teacherbranch').value;
    let Teachercost = document.getElementById('teachercost').value;

    fetch('http://localhost:60617/teachers', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Branch: Teacherbranch, Name: Teachername, Price: Teachercost })
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
    let TeachercostToUpd = document.getElementById('teachercostToUpdate').value;
    let Teacherbranch = teachers.find(t => t['id'] == teacherIdToUpdate)['branch'];
    let Teachername = teachers.find(t => t['id'] == teacherIdToUpdate)['name'];
    fetch('http://localhost:60617/teachers', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Name: Teachername, Branch: Teacherbranch, Price: TeachercostToUpd, Id: teacherIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}