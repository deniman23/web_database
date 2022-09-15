async function getUsers() {
    // отправляет запрос и получаем ответ
    const response = await fetch("/api/persons", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    // если запрос прошел нормально
    if (response.ok === true) {
        // получаем данные
        const persons = await response.json();
        const rows = document.querySelector("tbody");
        // добавляем полученные элементы в таблицу
        persons.forEach(person => rows.append(row(person)));
    }
}
async function getUser(id) {
    const response = await fetch(`/api/persons/${id}`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const person = await response.json();
        document.getElementById("personId").value = person.id;
        document.getElementById("personeName").value = person.name;
        document.getElementById("personPhone").value = person.mobile_phone;
        document.getElementById("personJob").value = person.personJob;
        document.getElementById("personBirthDate").value = person.personBirthDate;
    }
    else {
        // если произошла ошибка, получаем сообщение об ошибке
        const error = await response.json();
        console.log(error.message); // и выводим его на консоль
    }
}
async function createUser(personeName, personPhone, personJob, personBirthDate) {
 
    const response = await fetch("api/persons", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            name: personeName,
            mobile_phone: parseInt(personPhone,10),
            job_title: personJob,
            Birth_dateBirth_date: parseInt(personBirthDate, 10),
        })
    });
    if (response.ok === true) {
        const person = await response.json();
        document.querySelector("tbody").append(row(person));
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }
}
async function editUser(personId, personeName, personPhone, personJob, personBirthDate) {
    const response = await fetch("api/persons", {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            id: parseInt(personId,10),
            name: personeName,
            mobile_phone: parseInt(personPhone,10),
            job_title: personJob,
            Birth_dateBirth_date: parseInt(personBirthDate, 10),
        })
    });
    if (response.ok === true) {
        const person = await response.json();
        document.querySelector(`tr[data-rowid='${person.id}']`).replaceWith(row(person));
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }
}
