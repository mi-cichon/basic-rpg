<template>
  <div class="blog-thumb-img" @click="spawnSelected()">
    <div
      class="img"
      :class="{
        imgLast: spawnId == 0,
        imgLs: spawnId == 1,
        imgSandy: spawnId == 2,
        imgPaleto: spawnId == 3,
      }"
    ></div>
    <div class="blog-thumb-img-overlay">
      <span class="overlay-text"> {{ spawnName }} </span>
      <span class="overlay-link">
        <p>{{ $t("spawnSelection.clickText") }}</p>
      </span>
    </div>
  </div>
</template>

<script lang="ts">
export default {
  methods: {
    spawnSelected() {
      this.$emit("spawnSelected", this.spawnId);
    },
  },
  created() {},
  props: ["spawnName", "spawnId", "spawnImg"],
};
</script>

<style scoped>
.blog-thumb-img {
  overflow: hidden;
  position: relative;
  width: 18rem;
  height: 50rem;
  font-family: "Roboto";
  margin: 0.7rem;
  transform: skew(-7deg, 0deg);
}
.blog-thumb-img .img {
  transform: scale(1) skew(7deg, 0deg);
  transition: all 0.3s ease-in-out 0s;
  width: 200%;
  height: 200%;
  background-size: 250%;
  background-position-y: 0;
  background-position-x: 60%;
  background-repeat: no-repeat;
}

.imgLast {
  background-image: url("../../assets/images/lastpos.jpg");
}
.imgLs {
  background-image: url("../../assets/images/ls.jpg");
}
.imgSandy {
  background-image: url("../../assets/images/sandy.jpg");
}
.imgPaleto {
  background-image: url("../../assets/images/paleto.jpg");
}

.blog-thumb-img:hover .blog-thumb-img-overlay {
  opacity: 1;
  visibility: visible;
}
.blog-thumb-img:hover .blog-thumb-img-overlay .overlay-text,
.blog-thumb-img:hover .blog-thumb-img-overlay .overlay-link {
  transform: translateY(0px);
}
.blog-thumb-img:hover .img {
  transform: scale(1.1) skew(7deg, 0deg);
}
.blog-thumb-img-overlay {
  background: none repeat scroll 0 0 rgb(18, 25, 44, 0.6);
  bottom: 0;
  box-sizing: border-box;
  color: #ffffff;
  font-size: 1.45em;
  opacity: 0;
  padding: 50px 25px;
  position: absolute;
  text-align: left;
  top: 0;
  transition: all 0.2s ease-in 0s;
  visibility: hidden;
  width: 100%;
}
.blog-thumb-img-overlay .overlay-text {
  transform: translateX(-100%);
}
.blog-thumb-img-overlay .overlay-link {
  transform: translateX(100%);
}
.blog-thumb-img-overlay .overlay-text {
  font-weight: normal;
  display: block;
  line-height: 1.3em;
  transition: all 0.3s ease-in 0s;
  font-size: 2rem;
}
.blog-thumb-img-overlay .overlay-link {
  transition: all 0.3s ease-in 0s;
  display: block;
  color: #fff;
  font-size: 1rem;
  text-decoration: none;
  padding-top: 10px;
  position: absolute;
  bottom: 2rem;
}
</style>
