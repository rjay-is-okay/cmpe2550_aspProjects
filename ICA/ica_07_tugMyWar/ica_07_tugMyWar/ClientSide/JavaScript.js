$().ready(()=>{
    console.log("On Load");

    $("#placed").on("click",postSubmit);
})

function postSubmit(){
    console.log("post submit clicked");

    let url = "https://localhost:7180/registerPost";

    let data = {};

    data.postName = $("#givedName").val();
    data.postItem = $("#choice").val();
    data.postQuantity = $("#amount").val();
    data.postPayType = $("#pay").val();
    data.postLocation = $("#loc").val();


    console.log(data);

    //uncomment the following to test it for html
    //AJAX(url, "POST", data, "html", ProcessSuccess, ProcessError);

    //uncomment the following to test it for json
    AJAX(url, "POST", data, "json", ProcessSuccess, ProcessError);
}

function AJAX(url,method,data,dataType,successMethod,errorMethod){
    let ajaxOptions = {};
    ajaxOptions['url'] = url;
    ajaxOptions['method'] = method;
    ajaxOptions['data'] = JSON.stringify(data); //new for c#
    ajaxOptions['dataType'] = dataType;
    ajaxOptions['success'] = successMethod;
    ajaxOptions['error'] = errorMethod;
    ajaxOptions['contentType'] = "application/json"; //new for c#

    console.log(ajaxOptions);
    $.ajax(ajaxOptions);
}

function ProcessSuccess(returnedData, status){
    console.log("Success Ajax call");
    console.log(returnedData);
    let newDiv = $("#orderDetails");
    $(newDiv).empty();
    let nameHeader = document.createElement("h3");
    $(nameHeader).append(`Thank you ${returnedData.name} for placing this order.`);

    let newList = document.createElement("ol");
    $.each((returnedData),(index,key) => {
        console.log(index);
        console.log(key);
        let newOption = document.createElement("li");
        $(newOption).append(key);
        if (index != "name" && index != "time") {
            $(newList).append(newOption);
        }
    })
    let newFoot = document.createElement("p");
    $(newFoot).append(`Your order will be ready for pick up in ${returnedData.time} minutes.`);
    $(newDiv).append(nameHeader);
    $(newDiv).append(newList);
    $(newDiv).append(newFoot);
}

function ProcessError(returnedData, status){
    console.log("Error Ajax call");
    console.log(returnedData);
}