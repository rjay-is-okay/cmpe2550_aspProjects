$().ready(()=>{
    console.log("On Load");

    $("#postSubmit").on("click",postSubmit);
})

function postSubmit(){
    console.log("post submit clicked");

    let url = "https://localhost:7180/registerPost";

    let data = {};

    data.postName = $("#givedName").val();
    data.postItem = $("#postColor").val();
    data.postAge = $("#postAge").val();

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
}

function ProcessError(returnedData, status){
    console.log("Error Ajax call");
    console.log(returnedData);
}