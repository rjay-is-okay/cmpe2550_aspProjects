$().ready(()=>{
    console.log("On Load");
    postResume();
    $("#newG").on("click", postStart);
    $("#reseti").on("click", postQuit);
})

function postStart(){
    console.log("post Start clicked");

    let url = "https://localhost:7107/startNewGame";

    let data = {};

    data.postPlay1 = $("#player1").val();
    data.postPlay2 = $("#player2").val();

    console.log(data);

    //uncomment the following to test it for html
    //AJAX(url, "POST", data, "html", ProcessSuccess, ProcessError);

    //uncomment the following to test it for json
    AJAX(url, "POST", data, "json", ProcessStart, ProcessError);
}

function postResume() {
    console.log("post resume activated");

    let url = "https://localhost:7107/resume";

    let data = {};

    data.postPlay1 = $("#player1").val();
    data.postPlay2 = $("#player2").val();

    console.log(data);

    //uncomment the following to test it for html
    //AJAX(url, "POST", data, "html", ProcessSuccess, ProcessError);

    //uncomment the following to test it for json
    AJAX(url, "POST", data, "json", ProcessResume, ProcessError);
}

function postQuit() {
    console.log("post quit activated");
    $("#communication").html("Please Enter your names");

    let url = "https://localhost:7107/quitGame";

    let data = {};

    console.log(data);

    //uncomment the following to test it for html
    //AJAX(url, "POST", data, "html", ProcessSuccess, ProcessError);

    //uncomment the following to test it for json
    AJAX(url, "POST", data, "json", processQuit, ProcessError);
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

function ProcessResume(returnedData,status) {
    console.log("Success Ajax call");
    console.log(returnedData);

    $("#player1").val(returnedData.player1);
    $("#player2").val(returnedData.player2);

    if (returnedData.player1 != "" && returnedData.player2 != "") {
        $("#communication").html("Continue On");
        createBoard();
        $("img").css("grid-column-start", returnedData.location);
        if (returnedData.gameState != "neither") {
            $("#pull").remove();
            $("#communication").html(returnedData.gameState);
        }

    }
}

function processQuit(returnedData, status) {
    $("#gameBoard").empty();
    console.log("Success Ajax call");
    console.log(returnedData);

    $("#player1").val(returnedData.player1);
    $("#player2").val(returnedData.player2);
}

function ProcessStart(returnedData, status){
    console.log("Success Ajax call");
    console.log(returnedData);

    if (returnedData.start) {
        $("#gameBoard").empty();
        $("#communication").html("Let's go");
        createBoard();

    } else {
        $("#communication").html("Both names must be valid");
        $("#gameBoard").empty();
    }


}

function PostTug() {
    console.log("post tug activated");

    let url = "https://localhost:7107/registerMove";

    let data = {};

    console.log(data);

    //uncomment the following to test it for html
    //AJAX(url, "POST", data, "html", ProcessSuccess, ProcessError);

    //uncomment the following to test it for json
    AJAX(url, "POST", data, "json", processTug, ProcessError);
}

function processTug(returnedData, status) {

    $("img").css("grid-column-start", returnedData.location);
    if (returnedData.gameState != "neither") {
        $("#communication").html(returnedData.gameState);
        $("#pull").remove();
    }
}
function ProcessError(returnedData, status){
    console.log("Error Ajax call");
    console.log(returnedData);
}

function createBoard() {
    var newDiv = document.createElement("div");
    $(newDiv).prop("class", "detailedDiv");
    $(newDiv).prop("id", "topDiv");
    for (var i = 1; i < 66; i++) {
        const newSpan = document.createElement("span");
        newSpan.innerHTML = i;
        $(newDiv).append(newSpan);
    }
    $("#gameBoard").append(newDiv);
    //var newPara = document.createElement("div"); 
    var newImg = document.createElement("img");

    newDiv = document.createElement("div");
    $(newDiv).prop("class", "detailedDiv");
    $(newDiv).prop("id", "botDiv");

    newImg.src = "images/tug_o_war.jpg";
    //newPara.append(newImg);
    $("#gameBoard").append(newImg);

    for (var i = 1; i < 66; i++) {
        const newSpan = document.createElement("span");
        newSpan.innerHTML = i;
        $(newDiv).append(newSpan);
    }
    $("#gameBoard").append(newDiv);
    var newButt = document.createElement("button");
    newDiv = document.createElement("div");
    $(newDiv).prop("class", "detailedDiv");

    $(newButt).prop("id", "pull");
    $(newButt).val("PULL");
    $(newButt).html("PULLLLLL");
    $(newDiv).append(newButt);
    $("#gameBoard").append(newDiv);
    $(newButt).on("click", PostTug);
}