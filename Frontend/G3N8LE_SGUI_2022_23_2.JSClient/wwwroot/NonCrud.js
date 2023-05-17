let artistsearnings = [];
let mostpaidartist = [];
let lesspaidartist = [];
let bestfan = [];
let worstfan = [];
let deciding = null;



async function BestStudent() {
    document.getElementById('resultarea').innerHTML = "";
    document.getElementById('headresult').innerHTML = "";
    await fetch('http://localhost:60617/Noncrudfan/BestStudents')
        .then(x => x.json())
        .then(y => {
            bestfan = y;
            deciding = "BF";
            display(deciding);
        });
}
async function WorstStudent() {
    document.getElementById('resultarea').innerHTML = "";
    document.getElementById('headresult').innerHTML = "";
    await fetch('http://localhost:60617/Noncrudfan/WorstStudents')
        .then(x => x.json())
        .then(y => {
            worstfan = y;
            deciding = "WF";
            display(deciding);
        });
}
async function TeacherEarnings() {
    document.getElementById('resultarea').innerHTML = "";
    document.getElementById('headresult').innerHTML = "";
    await fetch('http://localhost:60617/Noncrudteacher/TeachersEarnings')
        .then(x => x.json())
        .then(y => {
            artistsearnings = y;
            deciding = "AE";
            display(deciding);
        });
}
async function Mostpaid() {
    document.getElementById('resultarea').innerHTML = "";
    document.getElementById('headresult').innerHTML = "";
    await fetch('http://localhost:60617/Noncrudteacher/MostPaidTeachers')
        .then(x => x.json())
        .then(y => {
            mostpaidartist = y;
            deciding = "MP";
            display(deciding);
        });
}
async function Lesspaid() {
    document.getElementById('resultarea').innerHTML = "";
    document.getElementById('headresult').innerHTML = "";
    await fetch('http://localhost:60617/Noncrudteacher/LessPaidTeachers')
        .then(x => x.json())
        .then(y => {
            lesspaidartist = y;
            deciding = "LP";
            display(deciding);
        });
}
function display() {
    if (deciding === "BS") {
        document.getElementById('headresult').innerHTML += "<tr><th>Student Id</th><th>Number of reservations</th></tr> ";
        document.getElementById('resultarea').innerHTML = "";
        beststudent.forEach(t => {
            document.getElementById('resultarea').innerHTML += "<tr><td>" + t.key + "</td><td>" + t.value + "</td></tr>";
        });
    }
    else if (deciding === "WS") {
        document.getElementById('headresult').innerHTML += "<tr><th>Student Id</th><th>Number of reservations</th></tr> ";
        document.getElementById('resultarea').innerHTML = "";
        worststudent.forEach(t => {
            document.getElementById('resultarea').innerHTML += "<tr><td>" + t.key + "</td><td>" + t.value + "</td></tr>";
        });
    }
    else if (deciding === "TE") {
        document.getElementById('headresult').innerHTML += "<tr><th>Teacher Name</th><th>Overall Earnings</th></tr> ";
        document.getElementById('resultarea').innerHTML = "";
        teachersearnings.forEach(t => {
            document.getElementById('resultarea').innerHTML += "<tr><td>" + t.key + "</td><td>" + t.value + " $</td></tr>";
        });
    }
    else if (deciding === "MP") {
        document.getElementById('headresult').innerHTML += "<tr><th>Teacher Name</th><th>Overall Earnings</th></tr> ";
        document.getElementById('resultarea').innerHTML = "";
        mostpaidteacher.forEach(t => {
            document.getElementById('resultarea').innerHTML += "<tr><td>" + t.key + "</td><td>" + t.value + " $</td></tr>";
        });
    }
    else if (deciding === "LP") {
        document.getElementById('headresult').innerHTML += "<tr><th>Teacher Name</th><th>Overall Earnings</th></tr> ";
        document.getElementById('resultarea').innerHTML = "";
        lesspaidteacher.forEach(t => {
            document.getElementById('resultarea').innerHTML += "<tr><td>" + t.key + "</td><td>" + t.value + " $</td></tr>";
        });
    }

}