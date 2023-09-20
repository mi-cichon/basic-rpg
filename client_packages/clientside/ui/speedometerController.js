let player = mp.players.local;

let getBrowser = () => {
    let b = mp.browsers.at(0);
    return b != null && mp.browsers.exists(b) ? b : null;
}

const rpc = require('ext/rage-rpc.js');

mp.events.add("render", () => {
    if(player.vehicle && getBrowser() != null){
        const browser = getBrowser();
        const speed = parseInt(player.vehicle.getSpeed() * 3.6);
        const rpm = player.vehicle.rpm * 0.92;
        const petrol = 0.5;
        const trip = 213.7;
        // let currentStreet = mp.game.pathfind.getStreetNameAtCoord(player.position.x, player.position.y, player.position.z, 0, 0);
        // let streetName = mp.game.ui.getStreetNameFromHashKey(currentStreet.streetName);]
        const speedometerData = {speed: speed, rpm: rpm, petrol: petrol, trip: trip};
        
        rpc.callBrowser(browser, 'browser_updateSpeedometer', {ResponseType: 0, Message: '', Data: speedometerData});
    }
});

mp.events.add("playerEnterVehicle", (vehicle, seat) => {
    let browser = getBrowser();
    if(browser == null){
        return;
    }
    rpc.callBrowser(browser, 'browser_displaySpeedometer', {ResponseType: 0, Message: '', Data: {state: true}});
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
    rpc.callBrowser(browser, 'browser_displaySpeedometer', {ResponseType: 0, Message: '', Data: {state: false}});
}