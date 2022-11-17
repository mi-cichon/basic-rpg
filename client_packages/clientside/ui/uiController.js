let player = mp.players.local;
const rpc = require('ext/rage-rpc.js');
let uiBrowser = null;

mp.discord.update('Gram w napad na bank' ,'Mam G-klase i klame.');

mp.events.add("client_createBrowser", () => {
    uiBrowser = mp.browsers.new("http://package2/index.html");
    loginUi();
});

mp.events.add("client_showNotification", (message, type) => {
    rpc.callBrowser(uiBrowser, 'ui_showNotification', {message: message, type: type});
});

mp.events.add("client_updateHudValues", values => {
    rpc.callBrowser(uiBrowser, 'ui_updateHudValues', values);
});

function loginUi(){
    player.freezePosition(true);

    mp.game.ui.displayHud(false);
    mp.game.ui.displayRadar(false);

    let loginCamera = mp.cameras.new('default', new mp.Vector3(0,0,1240), new mp.Vector3(0,0,0), 40);
    loginCamera.pointAtCoord(10000,0,1240);
    loginCamera.setActive(true);
    mp.game.cam.renderScriptCams(true, false, 0, true, false);
    mp.gui.cursor.show(true, true);

    rpc.callBrowser(uiBrowser, 'ui_showLoginComponent', true);
}



