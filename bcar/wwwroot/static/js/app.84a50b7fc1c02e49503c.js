webpackJsonp([8],{"/kga":function(t,e,n){"use strict";function i(t){n("7zGH")}var s=n("JkZY"),o=(s.a,Boolean,String,String,Number,String,String,Boolean,Object,Boolean,{mixins:[s.a],name:"x-dialog",model:{prop:"show",event:"change"},props:{show:{type:Boolean,default:!1},maskTransition:{type:String,default:"vux-mask"},maskZIndex:[String,Number],dialogTransition:{type:String,default:"vux-dialog"},dialogClass:{type:String,default:"weui-dialog"},hideOnBlur:Boolean,dialogStyle:Object,scroll:{type:Boolean,default:!0,validator:function(t){return!0}}},computed:{maskStyle:function(){if(void 0!==this.maskZIndex)return{zIndex:this.maskZIndex}}},mounted:function(){"undefined"!=typeof window&&window.VUX_CONFIG&&"VIEW_BOX"===window.VUX_CONFIG.$layout&&(this.layout="VIEW_BOX")},watch:{show:function(t){this.$emit("update:show",t),this.$emit(t?"on-show":"on-hide"),t?this.addModalClassName():this.removeModalClassName()}},methods:{shouldPreventScroll:function(){var t=/iPad|iPhone|iPod/i.test(window.navigator.userAgent),e=this.$el.querySelector("input")||this.$el.querySelector("textarea");if(t&&e)return!0},hide:function(){this.hideOnBlur&&(this.$emit("update:show",!1),this.$emit("change",!1),this.$emit("on-click-mask"))}},data:function(){return{layout:""}}}),a=function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("div",{staticClass:"vux-x-dialog",class:{"vux-x-dialog-absolute":"VIEW_BOX"===t.layout}},[n("transition",{attrs:{name:t.maskTransition}},[n("div",{directives:[{name:"show",rawName:"v-show",value:t.show,expression:"show"}],staticClass:"weui-mask",style:t.maskStyle,on:{click:t.hide}})]),t._v(" "),n("transition",{attrs:{name:t.dialogTransition}},[n("div",{directives:[{name:"show",rawName:"v-show",value:t.show,expression:"show"}],class:t.dialogClass,style:t.dialogStyle},[t._t("default")],2)])],1)},r=[],u={render:a,staticRenderFns:r},l=u,c=n("VU/8"),d=i,h=c(o,l,!1,d,null,null);e.a=h.exports},"3iZP":function(t,e){},"55wj":function(t,e){},"7zGH":function(t,e){},Bfwr:function(t,e,n){"use strict";function i(t){n("3iZP")}var s=(Boolean,String,String,String,{name:"loading",model:{prop:"show",event:"change"},props:{show:Boolean,text:String,position:String,transition:{type:String,default:"vux-mask"}},watch:{show:function(t){this.$emit("update:show",t)}}}),o=function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("transition",{attrs:{name:t.transition}},[n("div",{directives:[{name:"show",rawName:"v-show",value:t.show,expression:"show"}],staticClass:"weui-loading_toast vux-loading",class:t.text?"":"vux-loading-no-text"},[n("div",{staticClass:"weui-mask_transparent"}),t._v(" "),n("div",{staticClass:"weui-toast",style:{position:t.position}},[n("i",{staticClass:"weui-loading weui-icon_toast"}),t._v(" "),t.text?n("p",{staticClass:"weui-toast__content"},[t._v(t._s(t.text||"加载中")),t._t("default")],2):t._e()])])])},a=[],r={render:o,staticRenderFns:a},u=r,l=n("VU/8"),c=i,d=l(s,u,!1,c,null,null);e.a=d.exports},NHnr:function(t,e,n){"use strict";function i(t){n("RjyN")}Object.defineProperty(e,"__esModule",{value:!0});var s=n("7+uW"),o=n("v5o6"),a=n.n(o),r=n("/ocq"),u={name:"app"},l=function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("div",{attrs:{id:"app"}},[n("router-view")],1)},c=[],d={render:l,staticRenderFns:c},h=d,m=n("VU/8"),p=i,w=m(u,h,!1,p,null,null),f=w.exports,v=n("Y+qm"),g=n("Peep"),_=n("3BeM"),x=n("n6Wb");n("YN+o");s.a.use(_.a),s.a.use(v.a),s.a.use(g.a),s.a.use(x.a),s.a.use(r.a);var S=[{path:"/",component:function(){return Promise.all([n.e(0),n.e(1)]).then(n.bind(null,"civT"))}},{path:"/carlist",component:function(){return Promise.all([n.e(0),n.e(4)]).then(n.bind(null,"2gjT"))}},{path:"/register",component:function(){return Promise.all([n.e(0),n.e(2)]).then(n.bind(null,"wKWr"))}},{path:"/changeRoute",component:function(){return Promise.all([n.e(0),n.e(6)]).then(n.bind(null,"7Gd/"))}},{path:"/orderlist",component:function(){return Promise.all([n.e(0),n.e(5)]).then(n.bind(null,"nvey"))}},{path:"/successpage",component:function(){return Promise.all([n.e(0),n.e(3)]).then(n.bind(null,"S485"))}}],y=new r.a({routes:S});a.a.attach(document.body),s.a.config.productionTip=!1,new s.a({router:y,render:function(t){return t(f)}}).$mount("#app-box")},RjyN:function(t,e){},"YN+o":function(t,e,n){"use strict";var i=n("//Fk"),s=n.n(i),o=n("mtWM"),a=n.n(o),r=a.a.create({baseURL:"http://baibiancx.top/",timeout:5e3});r.interceptors.request.use(function(t){return t},function(t){console.log(t),s.a.reject(t)}),r.interceptors.response.use(function(t){return t.data},function(t){return console.log("err"+t),s.a.reject(t)}),e.a=r},kllH:function(t,e){},mzja:function(t,e,n){"use strict";function i(t){n("55wj")}var s=n("/kga"),o=(s.a,Boolean,String,String,String,Boolean,String,String,Number,String,{name:"alert",components:{XDialog:s.a},created:function(){void 0!==this.value&&(this.showValue=this.value)},props:{value:Boolean,title:String,content:String,buttonText:String,hideOnBlur:{type:Boolean,default:!1},maskTransition:{type:String,default:"vux-mask"},dialogTransition:{type:String,default:"vux-dialog"},maskZIndex:[Number,String]},data:function(){return{showValue:!1}},methods:{_onHide:function(){this.showValue=!1}},watch:{value:function(t){this.showValue=t},showValue:function(t){this.$emit("input",t)}}}),a=function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("div",{staticClass:"vux-alert"},[n("x-dialog",{attrs:{"mask-transition":t.maskTransition,"dialog-transition":t.dialogTransition,"hide-on-blur":t.hideOnBlur,"mask-z-index":t.maskZIndex},on:{"on-hide":function(e){return t.$emit("on-hide")},"on-show":function(e){return t.$emit("on-show")}},model:{value:t.showValue,callback:function(e){t.showValue=e},expression:"showValue"}},[n("div",{staticClass:"weui-dialog__hd"},[n("strong",{staticClass:"weui-dialog__title"},[t._v(t._s(t.title))])]),t._v(" "),n("div",{staticClass:"weui-dialog__bd"},[t._t("default",[n("div",{domProps:{innerHTML:t._s(t.content)}})])],2),t._v(" "),n("div",{staticClass:"weui-dialog__ft"},[n("a",{staticClass:"weui-dialog__btn weui-dialog__btn_primary",attrs:{href:"javascript:;"},on:{click:t._onHide}},[t._v(t._s(t.buttonText||"确定"))])])])],1)},r=[],u={render:a,staticRenderFns:r},l=u,c=n("VU/8"),d=i,h=c(o,l,!1,d,null,null);e.a=h.exports},rLAy:function(t,e,n){"use strict";function i(t){n("kllH")}var s=n("xNvf"),o=(s.a,Boolean,Number,String,String,String,Boolean,String,String,{name:"toast",mixins:[s.a],props:{value:Boolean,time:{type:Number,default:2e3},type:{type:String,default:"success"},transition:String,width:{type:String,default:"7.6em"},isShowMask:{type:Boolean,default:!1},text:String,position:String},data:function(){return{show:!1}},created:function(){this.value&&(this.show=!0)},computed:{currentTransition:function(){return this.transition?this.transition:"top"===this.position?"vux-slide-from-top":"bottom"===this.position?"vux-slide-from-bottom":"vux-fade"},toastClass:function(){return{"weui-toast_forbidden":"warn"===this.type,"weui-toast_cancel":"cancel"===this.type,"weui-toast_success":"success"===this.type,"weui-toast_text":"text"===this.type,"vux-toast-top":"top"===this.position,"vux-toast-bottom":"bottom"===this.position,"vux-toast-middle":"middle"===this.position}},style:function(){if("text"===this.type&&"auto"===this.width)return{padding:"10px"}}},watch:{show:function(t){var e=this;t&&(this.$emit("input",!0),this.$emit("on-show"),this.fixSafariOverflowScrolling("auto"),clearTimeout(this.timeout),this.timeout=setTimeout(function(){e.show=!1,e.$emit("input",!1),e.$emit("on-hide"),e.fixSafariOverflowScrolling("touch")},this.time))},value:function(t){this.show=t}}}),a=function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("div",{staticClass:"vux-toast"},[n("div",{directives:[{name:"show",rawName:"v-show",value:t.isShowMask&&t.show,expression:"isShowMask && show"}],staticClass:"weui-mask_transparent"}),t._v(" "),n("transition",{attrs:{name:t.currentTransition}},[n("div",{directives:[{name:"show",rawName:"v-show",value:t.show,expression:"show"}],staticClass:"weui-toast",class:t.toastClass,style:{width:t.width}},[n("i",{directives:[{name:"show",rawName:"v-show",value:"text"!==t.type,expression:"type !== 'text'"}],staticClass:"weui-icon-success-no-circle weui-icon_toast"}),t._v(" "),t.text?n("p",{staticClass:"weui-toast__content",style:t.style,domProps:{innerHTML:t._s(t.text)}}):n("p",{staticClass:"weui-toast__content",style:t.style},[t._t("default")],2)])])],1)},r=[],u={render:a,staticRenderFns:r},l=u,c=n("VU/8"),d=i,h=c(o,l,!1,d,null,null);e.a=h.exports}},["NHnr"]);
//# sourceMappingURL=app.84a50b7fc1c02e49503c.js.map