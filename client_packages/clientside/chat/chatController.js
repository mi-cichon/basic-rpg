let getBrowser = () => {
    let b = mp.browsers.at(0);
    return b != null && mp.browsers.exists(b) ? b : null;
}

let inputShown = false;

const rpc = require('ext/rage-rpc.js');

mp.keys.bind(0x54, false, () => {
    let browser = getBrowser();
    if(browser == null){
        return
    }
    rpc.callBrowser(browser, 'ui_showChatInput', true);
    inputShown = true;
});

mp.keys.bind(0x1B, false, () => {
    let browser = getBrowser();
    if(browser == null){
        return
    }
    rpc.callBrowser(browser, 'ui_showChatInput', false);
    inputShown = false;
})

mp.events.add('render', () => {
    if(inputShown){
        mp.game.controls.disableAllControlActions(32);
    }
});

rpc.register('client_sendMessage', (message) => {
    inputShown = false;
    mp.events.callRemote("chat_sendMessage", message);
});

mp.events.add('client_displayMessage', (message) => {
    let browser = getBrowser();
    if(browser == null){
        return;
    }
    rpc.callBrowser(browser, 'ui_displayMessage', message);
});