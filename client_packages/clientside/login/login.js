let getBrowser = () => {
    let b = mp.browsers.at(0);
    return b != null && mp.browsers.exists(b) ? b : null;
}

const rpc = require('ext/rage-rpc.js');

rpc.register('client_tryLogin', (credentials) => {
    mp.events.callRemote("user_login", credentials[0], credentials[1]);
});

rpc.register('client_tryRegister', (credentials) => {
    mp.events.callRemote("user_register", credentials[0], credentials[1]);
});

mp.events.add("client_loginCompleted", () => {
    let uiBrowser = getBrowser();
    if(uiBrowser == null){
        return;
    }
    rpc.callBrowser(uiBrowser, 'ui_loginCompleted');

    setTimeout(() => {
        mp.game.ui.displayHud(true);
        mp.game.ui.displayRadar(true);
        mp.gui.cursor.show(false, false);
        mp.game.cam.renderScriptCams(false, false, 0, true, false);
        mp.players.local.freezePosition(false);
    },2000);
   
});

mp.events.add("client_registerSuccessful", () => {
    let uiBrowser = getBrowser();
    if(uiBrowser == null){
        return;
    }
    rpc.callBrowser(uiBrowser, 'ui_registerSuccessful');
});