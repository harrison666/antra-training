function show() {
    fetch("https://fakerestapi.azurewebsites.net/api/v1/Books")
    .then(function (response){
        return response.json()
    })
    .then(function (data) {
        var tbody = document.querySelector("tbody")
        for (var i = 0; i < data.length; i++) {
            var tr = document.createElement("tr");
            tr.innerHTML = "<td>" + data[i].id + "</td><td>" + 
            data[i].title + "</td><td>" + data[i].description + "</td><td>" +
            data[i].pageCount + "</td><td>" + data[i].excerpt + "</td><td>" + 
            data[i].publishDate + "</td>";
            tbody.appendChild(tr);
        }
    })
    .catch(function (err){
        console.log(err)
    })
}