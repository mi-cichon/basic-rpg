<template>
  <div class="speedometer_Body" ref="speedometerBody">
    <div class="speedometer_Name">{{ vehName }}</div>
    <div class="speedometer_Street">{{ street }}</div>
    <div class="speedometer_Speed">
      <div class="speed">{{ speed }}</div>
      <div class="bottom">KM/H</div>
    </div>
    <div class="speedometer_Trip" v-if="false">
      <div class="trip-6"><p>0</p></div>
      <div class="trip-5"><p>0</p></div>
      <div class="trip-4"><p>0</p></div>
      <div class="trip-3"><p>0</p></div>
      <div class="trip-2"><p>0</p></div>
      <div class="trip-1"><p>0</p></div>
      <div class="trip-0"><p>0</p></div>
    </div>
    <div class="speedometer_RPM"></div>
    <div class="speedometer_Petrol" v-if="false"></div>
    <div class="speedometer_PetrolAmount" v-if="false">0%</div>
    <div class="speedometer_PetrolBack" v-if="false"></div>
    <div
      class="speedometer_Pointer"
      v-bind:style="{
        transform: `translate(-50%, -50%) rotate(${speed * speedStep - 50}deg)`,
      }"
    >
      <div class="speedometer_Needle"></div>
    </div>
    <div
      class="line"
      :class="{ long: line.long }"
      v-bind:style="{
        left: line.positionX + 'px',
        top: line.positionY + 'px',
        transform: `translate(-50%, -50%) rotate(${line.angle}deg)`,
      }"
      v-for="line in lines"
      v-bind:key="line.positionX"
    ></div>
    <div
      class="label"
      v-bind:style="{
        left: label.positionX + 'px',
        top: label.positionY + 'px',
      }"
      v-for="label in labels"
      v-bind:key="label.positionX"
    >
      {{ label.text }}
    </div>
  </div>
</template>

<script lang="ts">
declare interface Line {
  positionX: number;
  positionY: number;
  long: boolean;
  angle: number;
}

declare interface Label {
  positionX: number;
  positionY: number;
  text: number;
}

export default {
  props: ["maxSpeed", "vehName"],
  data() {
    return {
      speedStep: 0,
      speed: 0,
      street: "",
      lines: [] as Line[],
      labels: [] as Label[],
      rpmOptions: {},
      rpmProgressbar: null,
      rpm: 0,
    };
  },
  methods: {
    createLines() {
      let step = parseFloat((270 / (this.maxSpeed / 20)).toFixed(1));
      let fractionStep = parseFloat((step / 4).toFixed(1));
      step = parseInt((fractionStep * 40).toFixed(0));
      fractionStep *= 10;

      for (let i = 400; i <= 3125; i += fractionStep) {
        this.createLine(i / 10, (i - 400) % (step / 2) == 0);
        if ((i - 400) % step == 0) {
          this.createLabel(i / 10, ((i - 400) / step) * 20);
        }
      }

      this.speedStep = 270 / this.maxSpeed;
    },
    createLine(angle: number, long: boolean) {
      let position = this.getPositionByAngle(angle, 0.92);
      this.lines.push({
        positionX: position.x,
        positionY: position.y,
        angle: angle,
        long: long,
      });
    },
    createLabel(angle: number, text: number) {
      let position = this.getPositionByAngle(angle, 0.75);
      this.labels.push({
        positionX: position.x,
        positionY: position.y,
        text: text,
      });
    },
    getPositionByAngle(angle: number, scale: number) {
      let radius = 0.5 * (this.$refs.speedometerBody as any).clientWidth;
      let position = {
        x: -1 * radius * Math.sin(angle * (Math.PI / 180)) * scale,
        y: radius * Math.cos(angle * (Math.PI / 180)) * scale,
      };
      position.x += radius;
      position.y += radius;
      return position;
    },
    update(speed: number, street: string) {
      this.speed = Math.round(speed);
      this.street = street;
    },
  },

  created() {
    this.$nextTick(() => {
      this.createLines();
    });
  },
};
</script>

<style scoped>
.svg_petrol {
  width: 100%;
  height: 100%;
  position: relative;
}

.svg_petrol path {
  position: absolute;
  right: 0;
  bottom: 0;
}
img {
  position: absolute;
  top: 5vh;
  bottom: 5vh;
}

.speedometer_Trip {
  display: none;
  flex-direction: row;
  justify-content: space-around;
  width: 5rem;
  height: 1rem;
  position: absolute;
  transform: translate(-50%, -50%);
  left: 50%;
  top: 35%;
}

.speedometer_Trip div {
  height: 100%;
  width: 13%;
  background-color: rgba(18, 25, 44, 0.9);
  color: white;
  font-size: 0.9rem;
  text-align: center;
  font-family: "Digi";
  position: relative;
}

.speedometer_Trip div p {
  margin: 0;
  position: absolute;
  transform: translate(-50%, -50%);
  left: 50%;
  top: 50%;
}

.speedometer_Trip div:last-child {
  background-color: rgb(78, 0, 0);
}
.speedometer_Name {
  color: white;
  font-family: "Open Sans Condensed", sans-serif;
  font-size: 1.6rem;
  position: absolute;
  right: 105%;
  bottom: 17%;
  text-align: right;
  width: 10rem;
  display: inline-block;
  text-shadow: 1px 1px 2px black;
}

.speedometer_Street {
  color: white;
  font-family: "Open Sans Condensed", sans-serif;
  font-size: 2rem;
  position: absolute;
  right: 100%;
  bottom: 5%;
  text-align: right;
  width: 26rem;
  display: inline-block;
  text-shadow: 1px 1px 2px black;
}

.speedometer_Body {
  position: absolute;
  right: 2vh;
  bottom: 4vh;
  width: 18em;
  height: 18em;
  border-radius: 50%;
  background-color: rgba(18, 25, 44, 0.9);
}

.speedometer_RPM {
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%) rotate(-50deg);
  width: 104%;
  height: 104%;
  border-radius: 50%;
}

.speedometer_Petrol {
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%) rotate(110deg) rotateY(180deg);
  width: 102%;
  height: 102%;
  border-radius: 50%;
  z-index: 1;
}

.speedometer_PetrolAmount {
  position: absolute;
  left: 50%;
  bottom: -12%;
  transform: translate(-50%, -50%);
  width: auto;
  height: auto;
  color: white;
  font-size: 1.3rem;
  z-index: 1;
  font-family: "Digi";
  text-shadow: 1px 1px 2px black;
}

.speedometer_PetrolBack {
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%) rotate(110deg) rotateY(180deg);
  width: 101%;
  height: 101%;
  border-radius: 50%;
  z-index: 0;
}

#gradient {
  box-shadow: inset 0 0 0 gold, 0 0 1em 1px #0c9;
}

.speedometer_Pointer {
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
  background-color: #323635;
  width: 1.6rem;
  height: 1.6rem;
  border-radius: 50%;
  z-index: 10;
  border: 1px solid #0c9;
  box-shadow: inset 0 0 0 gold, 0 0 0.4em 1px #0c9;
}

.speedometer_Needle {
  position: absolute;
  right: 100%;
  top: 50%;
  transform: translateY(-50%);
  width: 7rem;
  height: 0.3rem;
  background-color: #0c9;
  border-radius: 10% 0 10% 0;
  box-shadow: inset 0 0 0 gold, 0 0 20px 1px #0c9;
}

.speedometer_Speed {
  position: absolute;
  left: 50%;
  bottom: -12%;
  transform: translate(-50%, -50%);
  width: auto;
  height: auto;
  font-family: "Digi";
  font-size: 4.8rem;
  color: white;
  text-align: center;
}

.speedometer_Speed .bottom {
  font-size: 1rem;
}

.line {
  width: 0.08rem;
  height: 0.3rem;
  transform: translate(-50%, -50%);
  position: absolute;
  background-color: #0c9;
  box-shadow: inset 0 0 0 gold, 0 0 1em 1px #0c9;
}

.long {
  height: 0.5rem;
}

.label {
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-58%, -100%);
  color: white;
  font-size: 1rem;
  width: 1.3rem;
  height: 0.7rem;
}
</style>
