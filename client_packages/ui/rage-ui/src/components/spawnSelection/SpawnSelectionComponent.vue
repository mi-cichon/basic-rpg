<template>
  <div class="body">
    <SelectSpawnComponent
      v-if="$route.params.lastpos === 'true'"
      :spawn-id="0"
      :spawn-name="$t('spawnSelection.lastPosition')"
      :spawn-img="'lastpos.jpg'"
      @spawn-selected="(spawnId) => spawnSelected(spawnId)"
    >
    </SelectSpawnComponent>

    <SelectSpawnComponent
      :spawn-id="1"
      :spawn-name="'Los Santos'"
      :spawn-img="'ls.jpg'"
      @spawn-selected="(spawnId) => spawnSelected(spawnId)"
    >
    </SelectSpawnComponent>

    <SelectSpawnComponent
      :spawn-id="2"
      :spawn-name="'Sandy Shores'"
      :spawn-img="'sandy.jpg'"
      @spawn-selected="(spawnId) => spawnSelected(spawnId)"
    >
    </SelectSpawnComponent>

    <SelectSpawnComponent
      :spawn-id="3"
      :spawn-name="'Paleto Bay'"
      :spawn-img="'paleto.jpg'"
      @spawn-selected="(spawnId) => spawnSelected(spawnId)"
    >
    </SelectSpawnComponent>
  </div>
</template>

<script lang="ts">
import SelectSpawnComponent from "./SelectSpawnComponent.vue";
import rpc from "rage-rpc";

export default {
  data() {
    return {};
  },
  methods: {
    spawnSelected(spawnId: number) {
      rpc.callClient("client_spawnSelected", spawnId);
    },
  },
  components: { SelectSpawnComponent },
  props: ["lastpos"],
};
</script>

<style scoped>
.body {
  width: auto;
  height: auto;
  display: flex;
  position: absolute;
  justify-content: center;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
}
</style>
