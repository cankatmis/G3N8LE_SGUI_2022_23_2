let students = [];
let connection = null;
getdata();
setupSignalR();
let studentIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:60617/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("StudentCreated", (user, message) => {
        getdata();
    });

    connection.on("StudentDeleted", (user, message) => {
        getdata();
    });
    connection.on("StudentUpdated", (user, message) => {
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
    await fetch('http://localhost:60617/students')
        .then(x => x.json())
        .then(y => {
            students = y;
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    students.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>"
            + t.name + "</td><td>" + t.city + "</td><td>" + t.email + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update City</button>`
            + "</td></tr>";
    });
    document.getElementById('studentname').value = "";
    document.getElementById('studentcity').value = "";
    document.getElementById('studentemail').value = "";
}
function showupdate(id) {
    document.getElementById('studentcityToUpdate').value = students.find(t => t['id'] == id)['city'];
    document.getElementById('updateformdiv').style.display = 'flex';
    studentIdToUpdate = id;
}
function remove(id) {
    fetch('http://localhost:60617/students/' + id, {
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
    let Studentname = document.getElementById('studentname').value;
    let Studentcity = document.getElementById('studentcity').value;
    let Studentemail = document.getElementById('studentemail').value;

    fetch('http://localhost:60617/students', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { City: Studentcity, Name: Studentname, Email: Studentemail })
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
    let StudentcityToUpd = document.getElementById('studentcityToUpdate').value;
    let Studentemail = students.find(t => t['id'] == studentIdToUpdate)['email'];
    let Studentname = students.find(t => t['id'] == studentIdToUpdate)['name'];
    fetch('http://localhost:60617/students', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { Name: Studentname, Email: Studentemail, City: StudentcityToUpd, Id: studentIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
