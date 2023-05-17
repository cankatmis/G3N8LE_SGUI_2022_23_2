let reservations = [];
let connection = null;
getdata();
setupSignalR();
let reservationIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:60617/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ReservationCreated", (user, message) => {
        getdata();
    });

    connection.on("ReservationDeleted", (user, message) => {
        getdata();
    });
    connection.on("ReservationUpdated", (user, message) => {
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
    await fetch('http://localhost:60617/Reservations')
        .then(x => x.json())
        .then(y => {
            reservations = y;
            display();
        });
}
function display() {
    document.getElementById('resultarea').innerHTML = "";
    reservations.forEach(t => {
        if (t.teacherId != null && t.studentId != null) {
            document.getElementById('resultarea').innerHTML +=
                "<tr><td>" + t.id + "</td><td>"
                + t.studentId + "</td><td>" + t.teacherId + "</td><td>" + t.dateTime + "</td><td>" +
                `<button type="button" onclick="remove(${t.id})">Delete</button>` +
                `<button type="button" onclick="showupdate(${t.id})">Update Date</button>`
                + "</td></tr>";
        }
    });
    document.getElementById('reservationstudentid').value = "";
    document.getElementById('reservationteacherid').value = "";
    document.getElementById('reservationdate').value = "";

}
function showupdate(id) {
    document.getElementById('reservationdateToUpdate').value = reservations.find(t => t['id'] == id)['dateTime'];
    document.getElementById('updateformdiv').style.display = 'flex';
    reservationIdToUpdate = id;
}
function remove(id) {
    fetch('http://localhost:60617/reservations/' + id, {
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
    let Reserstudentid = document.getElementById('reservationstudentid').value;
    let Reserteacherid = document.getElementById('reservationteacherid').value;
    let Reserdate = document.getElementById('reservationdate').value;

    fetch('http://localhost:60617/reservations', {
        method: 'POST',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { StudentId: Reserstudentid, TeacherId: Reserteacherid, DateTime: Reserdate })
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
    let ReserdateToUpd = document.getElementById('reservationdateToUpdate').value;
    let Reserstudentid = reservations.find(t => t['id'] == reservationIdToUpdate)['studentid'];
    let Reserteacherid = reservations.find(t => t['id'] == reservationIdToUpdate)['teacherid'];
    fetch('http://localhost:60617/Reservations', {
        method: 'PUT',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(
            { StudentId: Reserstudentid, TeacherId: Reserteacherid, DateTime: ReserdateToUpd, Id: reservationIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}