var mList = {
    Toyota : ['Corolla', 'Yaris', 'RAV4', 'Camry'],
    BMW :  ['i8', 'M5', 'X1', 'X2', 'X3'],
    GM :  ['Bolt EV', 'Camaro', 'Colorado'],
    Ford :  ['Mustang', 'Explorer', 'Ranger']
};
var pList = {
    Toyota : {'Corolla':'1000', 'Yaris':'2000', 'RAV4':'3000', 'Camry':'500'},
    BMW :  {'i8':'1000', 'M5':'2000', 'X1':'3000', 'X2':'500'},
    GM :  {'Bolt EV':'1000', 'Camaro':'2000','Colorado':'3000'},
    Ford :  {'Mustang':'1000', 'Explorer':'2000', 'Ranger':'3000'},
};


brands = document.getElementById("brands");
models = document.getElementById("models");


brands.addEventListener('change', function populate_child(e){
    models.innerHTML = '';
    itm = e.target.value;
    if(itm in mList){
            for (i = 0; i < mList[itm].length; i++) {
                models.innerHTML += '<option>'+ mList[itm][i] +'</option>';
            }
    }
});

btnShowPrice = document.getElementById("showPrice")
divSummary = document.getElementById("summary")
textName = document.getElementById("name")
textEmail = document.getElementById("email")
btnShowPrice.addEventListener("click", function () {
    divSummary.innerHTML = ""
    divSummary.innerHTML += "<h2>Summary</h2>" + 
    "Name: " + textName.value + "<br>" + 
    "Email: " + textEmail.value + "<br>" + 
    "Brand: " + brands.value + "<br>" + 
    "Model: " + models.value + "<br>" + 
    "Price: " + pList[brands.value][models.value];
    console.log(pList[brands.value]);
    console.log(models.value);

})