let localPlayer = mp.players.local;
let nametags = [];

mp.events.add("render", () => {
    nametags.forEach(x => x.update());
});

class NameTag {
    constructor(player){
        this.player = player;
        this.name = player.hasVariable('player_name') 
                        ? player.getVariable('player_name') 
                        : '';
        this.permissions = player.hasVariable('player_permissions') 
                        ? player.getVariable('player_permissions') 
                        : -1;
        this.label = this.name != '' 
            ? mp.labels.new(this.name + `[${this.player.remoteId}]`, this.getPosition(), {
                font: 4,
                los: false
            }) 
            : null;
    }

    update(){
        if(this.name == ''){
            this.name = this.player.hasVariable('player_name') 
                ? this.player.getVariable('player_name') 
                : '';
        }

        if(this.permissions == -1){
            this.permissions = this.player.hasVariable('player_permissions') 
                ? this.player.getVariable('player_permissions') 
                : -1;
        }
        
        if(this.label == null){
            this.label = this.name != '' 
                ? mp.labels.new(this.name + `[${this.player.remoteId}]`, this.getPosition(), {
                    font: 4,
                    los: false
                }) 
                : null;
        }
        
        if(this.label != null){
            this.label.position = this.getPosition();
        }
    }

    destroy(){
        this.label.destroy();
    }

    getPosition(){
        return this.player.getBoneCoords(12844, 0.4, 0, 0);
    }
}

nametags.push(new NameTag(localPlayer));

mp.events.add('entityStreamIn', (entity) => {
    if(entity.type === 'player'){
        nametags.push(new NameTag(entity));
    }
})

mp.events.add('entityStreamOut', (entity) => {
    if(entity.type === 'player'){
        nametags.forEach((x, i) => {
            if(x.player == entity){
                nametags.splice(i, 1);
            }
        })
    }
})