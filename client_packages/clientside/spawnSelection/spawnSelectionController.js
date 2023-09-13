let getBrowser = () => {
    let b = mp.browsers.at(0);
    return b != null && mp.browsers.exists(b) ? b : null;
}

const rpc = require('ext/rage-rpc.js');

rpc.register('client_spawnSelected', (data) => {
    mp.events.callRemote('user_spawnSelected', data.spawnId);
});

mp.events.add("client_spawnSelectionCompleted", (response) => {
    let uiBrowser = getBrowser();
    if(uiBrowser == null){
        return;
    }

    setTimeout(() => {
        mp.game.ui.displayHud(true);
        mp.game.ui.displayRadar(true);
        mp.gui.cursor.show(false, false);
        mp.game.cam.renderScriptCams(false, false, 0, true, false);
        mp.players.local.freezePosition(false);
    }, 2000);
    
    rpc.callBrowser(uiBrowser, 'response_client_spawnSelected', response);
})