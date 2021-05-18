function show() {
    fetch("https://fakerestapi.azurewebsites.net/api/v1/Users")
    .then(function (response){
        return response.json()
    })
    .then(function (data) {
        var tbody = document.querySelector("tbody")
        for (var i = 0; i < data.length; i++) {
            var tr = document.createElement("tr");
            tr.innerHTML = "<td>" + data[i].id + "</td><td>" + 
            data[i].userName + "</td><td>" + data[i].password + "</td>";
            tbody.appendChild(tr);
        }
    })
    .catch(function (err){
        console.log(err)
    })
}