mp.gui.chat.show(false);
mp.nametags.enabled = false;

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
});