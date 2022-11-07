"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/hubserver").build();
connection.on("ReloadProduct", function (){
    location.reload();
})

connection.start().then().catch(function (err) {
    return console.log(err.toString());
});