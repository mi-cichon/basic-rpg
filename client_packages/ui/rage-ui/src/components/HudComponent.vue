<template>
  <div class="body" v-if="hudValues.money != -1">
    <div class="content">
      <div class="name stat">{{ hudValues.name }}</div>
      <div class="experience stat">
        <i class="fa-solid fa-arrow-up-wide-short" style="color: gold"></i>
        {{ hudValues.experience }}{{ getNextExperience() }}
      </div>
      <div class="money stat">
        <i class="fa-solid fa-dollar-sign" style="color: lime"></i>
        {{ hudValues.money.toFixed(2) }}
      </div>
    </div>
    <div class="avatar">
      <div class="level">{{ hudValues.level }}</div>
    </div>
  </div>
</template>

<script lang="ts">
import rpc from "rage-rpc";

export default {
  props: {
    message: String,
    type: Number,
  },
  data() {
    return {
      hudValues: {
        money: -1,
        name: "",
        experience: 0,
        nextLevelExperience: 0,
        level: 1,
      },
    };
  },
  methods: {
    getNextExperience() {
      if (this.hudValues.nextLevelExperience != -1) {
        return ` / ${this.hudValues.nextLevelExperience}`;
      } else {
        return "";
      }
    },
  },
  created() {
    rpc.register("ui_updateHudValues", (values) => {
      this.hudValues = values;
    });
  },
};
</script>

<style scoped>
.body {
  position: absolute;
  width: 24rem;
  height: 10rem;
  top: 2rem;
  right: 2rem;
  display: flex;
  justify-content: space-around;
  align-items: center;
}

.content {
  width: 16rem;
  height: 70%;
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: flex-end;
}

.stat {
  font-family: "Roboto";
  background-color: rgba(18, 25, 44, 0.9);
  color: white;
  width: auto;
  height: 2rem;
  line-height: 2rem;
  padding-right: 0.7rem;
  padding-left: 0.7rem;
  border-radius: 15px;
  font-size: 1.2rem;
}

.experience {
  margin-right: 0.5rem;
}

.level {
  font-family: "Roboto";
  background-color: rgba(18, 25, 44, 1);
  color: white;
  height: 2rem;
  width: 2rem;
  line-height: 2rem;
  border-radius: 15px;
  font-size: 1.2rem;
  text-align: center;
  position: absolute;
  left: 50%;
  bottom: -1.2rem;
  transform: translateX(-50%);
}

.avatar {
  width: 8rem;
  height: 8rem;
  background-image: url("./../assets/images/avatar.png");
  background-position: center;
  background-size: cover;
  background-repeat: no-repeat;
  border-radius: 50%;
  border: 8px solid #12192c;
  position: relative;
}
</style>
