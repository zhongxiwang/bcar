webpackJsonp([3],{"3iZP":function(t,e){},"55wj":function(t,e){},"7zGH":function(t,e){},Bfwr:function(t,e,i){"use strict";function n(t){i("3iZP")}var o=(Boolean,String,String,String,{name:"loading",model:{prop:"show",event:"change"},props:{show:Boolean,text:String,position:String,transition:{type:String,default:"vux-mask"}},watch:{show:function(t){this.$emit("update:show",t)}}}),s=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("transition",{attrs:{name:t.transition}},[i("div",{directives:[{name:"show",rawName:"v-show",value:t.show,expression:"show"}],staticClass:"weui-loading_toast vux-loading",class:t.text?"":"vux-loading-no-text"},[i("div",{staticClass:"weui-mask_transparent"}),t._v(" "),i("div",{staticClass:"weui-toast",style:{position:t.position}},[i("i",{staticClass:"weui-loading weui-icon_toast"}),t._v(" "),t.text?i("p",{staticClass:"weui-toast__content"},[t._v(t._s(t.text||"加载中")),t._t("default")],2):t._e()])])])},a=[],r={render:s,staticRenderFns:a},u=r,l=i("VU/8"),c=n,d=l(o,u,!1,c,null,null);e.a=d.exports},NHnr:function(t,e,i){"use strict";function n(t){i("RjyN")}Object.defineProperty(e,"__esModule",{value:!0});var o=i("7+uW"),s=i("v5o6"),a=i.n(s),r=i("/ocq"),u={name:"app"},l=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("div",{attrs:{id:"app"}},[i("router-view")],1)},c=[],d={render:l,staticRenderFns:c},h=d,w=i("VU/8"),m=n,p=w(u,h,!1,m,null,null),f=p.exports,v=i("Y+qm"),g=i("Peep"),_=i("3BeM"),x=i("n6Wb"),S=i("yD8N");i("YN+o");o.a.use(_.a),o.a.use(v.a),o.a.use(g.a),o.a.use(x.a),o.a.use(S.a);var y=["onMenuShareTimeline","onMenuShareAppMessage","onMenuShareQQ","onMenuShareWeibo","onMenuShareQZone","chooseImage","getNetworkType","hideOptionMenu","showOptionMenu","hideMenuItems","uploadImage","showMenuItems","hideAllNonBaseMenuItem","showAllNonBaseMenuItem","closeWindow"];o.a.http.get("http://baibiancx.top/apiticket?url="+window.location.href,function(t){console.log(t),alert(t),o.a.wechat.config({debug:!0,appId:"wx6a2e25ee7abfca60",timestamp:"1414587457",nonceStr:"Wm3WZYTPz0wzccnW",signature:t,jsApiList:y})}),o.a.use(r.a);var b=[{path:"/",component:function(){return i.e(0).then(i.bind(null,"civT"))}},{path:"/carlist",component:function(){return i.e(1).then(i.bind(null,"2gjT"))}}],k=new r.a({routes:b});a.a.attach(document.body),o.a.config.productionTip=!1,new o.a({router:k,render:function(t){return t(f)}}).$mount("#app-box")},RjyN:function(t,e){},"YN+o":function(t,e,i){"use strict";var n=i("//Fk"),o=i.n(n),s=i("mtWM"),a=i.n(s),r=a.a.create({baseURL:"http://baibiancx.top/",timeout:5e3});r.interceptors.request.use(function(t){return t},function(t){console.log(t),o.a.reject(t)}),r.interceptors.response.use(function(t){return t.data},function(t){return console.log("err"+t),o.a.reject(t)}),e.a=r},kllH:function(t,e){},mzja:function(t,e,i){"use strict";function n(t){i("7zGH")}function o(t){i("55wj")}var s=i("JkZY"),a=(s.a,Boolean,String,String,Number,String,String,Boolean,Object,Boolean,{mixins:[s.a],name:"x-dialog",model:{prop:"show",event:"change"},props:{show:{type:Boolean,default:!1},maskTransition:{type:String,default:"vux-mask"},maskZIndex:[String,Number],dialogTransition:{type:String,default:"vux-dialog"},dialogClass:{type:String,default:"weui-dialog"},hideOnBlur:Boolean,dialogStyle:Object,scroll:{type:Boolean,default:!0,validator:function(t){return!0}}},computed:{maskStyle:function(){if(void 0!==this.maskZIndex)return{zIndex:this.maskZIndex}}},mounted:function(){"undefined"!=typeof window&&window.VUX_CONFIG&&"VIEW_BOX"===window.VUX_CONFIG.$layout&&(this.layout="VIEW_BOX")},watch:{show:function(t){this.$emit("update:show",t),this.$emit(t?"on-show":"on-hide"),t?this.addModalClassName():this.removeModalClassName()}},methods:{shouldPreventScroll:function(){var t=/iPad|iPhone|iPod/i.test(window.navigator.userAgent),e=this.$el.querySelector("input")||this.$el.querySelector("textarea");if(t&&e)return!0},hide:function(){this.hideOnBlur&&(this.$emit("update:show",!1),this.$emit("change",!1),this.$emit("on-click-mask"))}},data:function(){return{layout:""}}}),r=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("div",{staticClass:"vux-x-dialog",class:{"vux-x-dialog-absolute":"VIEW_BOX"===t.layout}},[i("transition",{attrs:{name:t.maskTransition}},[i("div",{directives:[{name:"show",rawName:"v-show",value:t.show,expression:"show"}],staticClass:"weui-mask",style:t.maskStyle,on:{click:t.hide}})]),t._v(" "),i("transition",{attrs:{name:t.dialogTransition}},[i("div",{directives:[{name:"show",rawName:"v-show",value:t.show,expression:"show"}],class:t.dialogClass,style:t.dialogStyle},[t._t("default")],2)])],1)},u=[],l={render:r,staticRenderFns:u},c=l,d=i("VU/8"),h=n,w=d(a,c,!1,h,null,null),m=w.exports,p=(Boolean,String,String,String,Boolean,String,String,Number,String,{name:"alert",components:{XDialog:m},created:function(){void 0!==this.value&&(this.showValue=this.value)},props:{value:Boolean,title:String,content:String,buttonText:String,hideOnBlur:{type:Boolean,default:!1},maskTransition:{type:String,default:"vux-mask"},dialogTransition:{type:String,default:"vux-dialog"},maskZIndex:[Number,String]},data:function(){return{showValue:!1}},methods:{_onHide:function(){this.showValue=!1}},watch:{value:function(t){this.showValue=t},showValue:function(t){this.$emit("input",t)}}}),f=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("div",{staticClass:"vux-alert"},[i("x-dialog",{attrs:{"mask-transition":t.maskTransition,"dialog-transition":t.dialogTransition,"hide-on-blur":t.hideOnBlur,"mask-z-index":t.maskZIndex},on:{"on-hide":function(e){return t.$emit("on-hide")},"on-show":function(e){return t.$emit("on-show")}},model:{value:t.showValue,callback:function(e){t.showValue=e},expression:"showValue"}},[i("div",{staticClass:"weui-dialog__hd"},[i("strong",{staticClass:"weui-dialog__title"},[t._v(t._s(t.title))])]),t._v(" "),i("div",{staticClass:"weui-dialog__bd"},[t._t("default",[i("div",{domProps:{innerHTML:t._s(t.content)}})])],2),t._v(" "),i("div",{staticClass:"weui-dialog__ft"},[i("a",{staticClass:"weui-dialog__btn weui-dialog__btn_primary",attrs:{href:"javascript:;"},on:{click:t._onHide}},[t._v(t._s(t.buttonText||"确定"))])])])],1)},v=[],g={render:f,staticRenderFns:v},_=g,x=i("VU/8"),S=o,y=x(p,_,!1,S,null,null);e.a=y.exports},rLAy:function(t,e,i){"use strict";function n(t){i("kllH")}var o=i("xNvf"),s=(o.a,Boolean,Number,String,String,String,Boolean,String,String,{name:"toast",mixins:[o.a],props:{value:Boolean,time:{type:Number,default:2e3},type:{type:String,default:"success"},transition:String,width:{type:String,default:"7.6em"},isShowMask:{type:Boolean,default:!1},text:String,position:String},data:function(){return{show:!1}},created:function(){this.value&&(this.show=!0)},computed:{currentTransition:function(){return this.transition?this.transition:"top"===this.position?"vux-slide-from-top":"bottom"===this.position?"vux-slide-from-bottom":"vux-fade"},toastClass:function(){return{"weui-toast_forbidden":"warn"===this.type,"weui-toast_cancel":"cancel"===this.type,"weui-toast_success":"success"===this.type,"weui-toast_text":"text"===this.type,"vux-toast-top":"top"===this.position,"vux-toast-bottom":"bottom"===this.position,"vux-toast-middle":"middle"===this.position}},style:function(){if("text"===this.type&&"auto"===this.width)return{padding:"10px"}}},watch:{show:function(t){var e=this;t&&(this.$emit("input",!0),this.$emit("on-show"),this.fixSafariOverflowScrolling("auto"),clearTimeout(this.timeout),this.timeout=setTimeout(function(){e.show=!1,e.$emit("input",!1),e.$emit("on-hide"),e.fixSafariOverflowScrolling("touch")},this.time))},value:function(t){this.show=t}}}),a=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("div",{staticClass:"vux-toast"},[i("div",{directives:[{name:"show",rawName:"v-show",value:t.isShowMask&&t.show,expression:"isShowMask && show"}],staticClass:"weui-mask_transparent"}),t._v(" "),i("transition",{attrs:{name:t.currentTransition}},[i("div",{directives:[{name:"show",rawName:"v-show",value:t.show,expression:"show"}],staticClass:"weui-toast",class:t.toastClass,style:{width:t.width}},[i("i",{directives:[{name:"show",rawName:"v-show",value:"text"!==t.type,expression:"type !== 'text'"}],staticClass:"weui-icon-success-no-circle weui-icon_toast"}),t._v(" "),t.text?i("p",{staticClass:"weui-toast__content",style:t.style,domProps:{innerHTML:t._s(t.text)}}):i("p",{staticClass:"weui-toast__content",style:t.style},[t._t("default")],2)])])],1)},r=[],u={render:a,staticRenderFns:r},l=u,c=i("VU/8"),d=n,h=c(s,l,!1,d,null,null);e.a=h.exports}},["NHnr"]);
//# sourceMappingURL=app.707f3b802d511c7f5ba8.js.map