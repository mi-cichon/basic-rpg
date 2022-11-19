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

mp.events.add("client_loginCompleted", (hasLastPos) => {
    let uiBrowser = getBrowser();
    if(uiBrowser == null){
        return;
    }
    rpc.callBrowser(uiBrowser, 'ui_loginCompleted', hasLastPos);   
});

mp.events.add("client_registerSuccessful", () => {
    let uiBrowser = getBrowser();
    if(uiBrowser == null){
        return;
    }
    rpc.callBrowser(uiBrowser, 'ui_registerSuccessful');
});