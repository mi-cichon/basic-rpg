let player = mp.players.local;

let getBrowser = () => {
    let b = mp.browsers.at(0);
    return b != null && mp.browsers.exists(b) ? b : null;
}

const rpc = require('ext/rage-rpc.js');

mp.events.add("render", () => {
    // if(player.vehicle && getBrowser() != null){
    //     let browser = getBrowser();
    //     let speed = parseInt(player.vehicle.getSpeed() * 3.6);
    //     let currentStreet = mp.game.pathfind.getStreetNameAtCoord(player.position.x, player.position.y, player.position.z, 0, 0);
    //     let streetName = mp.game.ui.getStreetNameFromHashKey(currentStreet.streetName);
    //     rpc.callBrowser(browser, 'ui_updateSpeedometer', {speed: speed, street: streetName});
    // }
});

mp.events.add("playerEnterVehicle", (vehicle, seat) => {
    // let browser = getBrowser();
    // if(browser == null){
    //     return;
    // }
    // let maxSpeed = 320;
    // if(vehicle.hasVariable('vehicle_maxSpeed')){
    //     maxSpeed = vehicle.getVariable('vehicle_maxSpeed');
    // }
    // let vehicleName = mp.game.vehicle.getDisplayNameFromVehicleModel(player.vehicle.model);
    // if(vehicle.hasVariable('vehicle_name')){
    //     vehicleName = vehicle.getVariable('vehicle_name');
    // }
    // rpc.callBrowser(browser, 'ui_displaySpeedometer', {state: true, speed: maxSpeed, name: vehicleName});
});

mp.events.add("playerLeaveVehicle", (vehicle, seat) => {
    destroySpeedometer();
})

mp.events.add("client_destroySpeedometer", () => {
    destroySpeedometer();
});

function destroySpeedometer(){
    let browser = getBrowser();
    if(browser == null){
        return;
    }
    rpc.callBrowser(browser, 'ui_displaySpeedometer', {state: false, speed: null, name: null});
}