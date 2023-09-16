mp.gui.chat.show(false);
mp.nametags.enabled = false;
mp.storage.data.userSpawned = false;
mp.events.add('render', () => {
    mp.game.controls.disableControlAction(32, 36, true);
    mp.game.controls.disableControlAction(32, 44, true);
    mp.game.controls.disableControlAction(32, 37, true);
    mp.game.controls.disableControlAction(32, 140, true);
    mp.game.controls.disableControlAction(32, 141, true);
    mp.game.controls.disableControlAction(32, 142, true);
    mp.game.controls.disableControlAction(32, 345, true);
    mp.game.controls.disableControlAction(32, 346, true);
    mp.game.controls.disableControlAction(32, 347, true);
    mp.game.controls.disableControlAction(32, 23, true);
    mp.game.controls.disableControlAction(32, 68, true);
    mp.game.controls.disableControlAction(32, 99, true);
    mp.game.controls.disableControlAction(32, 100, true);
    mp.game.controls.disableControlAction(32, 25, true);
    mp.game.controls.disableControlAction(32, 24, true);
    mp.game.controls.disableControlAction(32, 91, true);
    mp.game.controls.disableControlAction(32, 92, true);

    mp.game.ui.hideHudComponentThisFrame(2);
    mp.game.ui.hideHudComponentThisFrame(3);
    mp.game.ui.hideHudComponentThisFrame(4);
    mp.game.ui.hideHudComponentThisFrame(7);
    mp.game.ui.hideHudComponentThisFrame(9);
    mp.game.ui.hideHudComponentThisFrame(6);
    mp.game.ui.hideHudComponentThisFrame(8);
    mp.game.ui.hideHudComponentThisFrame(14);

    
    mp.game.player.restoreStamina(100);
});

mp.keys.bind(0xBB, false, () => {
    if(mp.storage.data.userSpawned === true){
        mp.events.callRemote('user_spawnOppressor')
    }
})