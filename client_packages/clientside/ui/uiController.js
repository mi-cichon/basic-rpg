let player = mp.players.local;
const rpc = require('ext/rage-rpc.js');
let uiBrowser = null;

mp.discord.update('👀');

//Player connected
uiBrowser = mp.browsers.new("http://package2/index.html");
player.freezePosition(true);
mp.game.ui.displayHud(false);
mp.game.ui.displayRadar(false);
let loginCamera = mp.cameras.new('default', new mp.Vector3(0,0,1240), new mp.Vector3(0,0,0), 40);
loginCamera.pointAtCoord(10000,0,1240);
loginCamera.setActive(true);
mp.game.cam.renderScriptCams(true, false, 0, true, false);
setTimeout(async () => {
    mp.gui.cursor.visible = true;
}, 0);


mp.events.add("client_showNotification", (message, type) => {
    //rpc.callBrowser(uiBrowser, 'ui_showNotification', {message: message, type: type});
});

mp.events.add("client_updateHudValues", response => {
    rpc.callBrowser(uiBrowser, 'browser_updatePlayerInfo', response);
});