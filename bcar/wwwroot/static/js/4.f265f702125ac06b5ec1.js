webpackJsonp([4],{"0FxO":function(t,e,i){"use strict";function n(t,e){if(!/^javas/.test(t)&&t){"object"===(void 0===t?"undefined":a()(t))||e&&"string"==typeof t&&!/http/.test(t)?"object"===(void 0===t?"undefined":a()(t))&&!0===t.replace?e.replace(t):"BACK"===t?e.go(-1):e.push(t):window.location.href=t}}function o(t,e){return!e||e._history||"string"!=typeof t||/http/.test(t)?t&&"object"!==(void 0===t?"undefined":a()(t))?t:"javascript:void(0);":"#!"+t}e.b=n,e.a=o;var s=i("pFYg"),a=i.n(s)},"1Ahr":function(t,e){},"2gjT":function(t,e,i){"use strict";function n(t){i("nnSp")}function o(t){i("y0+N")}Object.defineProperty(e,"__esModule",{value:!0});var s=i("mvHQ"),a=i.n(s),r=i("T8/m"),l=i("rHil"),u=i("QHDY"),c=i("/kga"),d=(c.a,Boolean,Boolean,String,String,Boolean,String,String,String,String,Number,String,String,String,Boolean,Object,Boolean,String,Boolean,Boolean,{name:"confirm",components:{XDialog:c.a},props:{value:{type:Boolean,default:!1},showInput:{type:Boolean,default:!1},placeholder:{type:String,default:""},theme:{type:String,default:"ios"},hideOnBlur:{type:Boolean,default:!1},title:String,confirmText:String,cancelText:String,maskTransition:{type:String,default:"vux-fade"},maskZIndex:[Number,String],dialogTransition:{type:String,default:"vux-dialog"},content:String,closeOnConfirm:{type:Boolean,default:!0},inputAttrs:Object,showContent:{type:Boolean,default:!0},confirmType:{type:String,default:"primary"},showCancelButton:{type:Boolean,default:!0},showConfirmButton:{type:Boolean,default:!0}},created:function(){this.showValue=this.show,this.value&&(this.showValue=this.value)},watch:{value:function(t){this.showValue=t},showValue:function(t){var e=this;this.$emit("input",t),t&&(this.showInput&&(this.msg="",setTimeout(function(){e.$refs.input&&e.setInputFocus()},300)),this.$emit("on-show"))}},data:function(){return{msg:"",showValue:!1}},methods:{getInputAttrs:function(){return this.inputAttrs||{type:"text"}},setInputValue:function(t){this.msg=t},setInputFocus:function(t){t&&t.preventDefault(),this.$refs.input.focus()},_onConfirm:function(){this.showValue&&(this.closeOnConfirm&&(this.showValue=!1),this.$emit("on-confirm",this.msg))},_onCancel:function(){this.showValue&&(this.showValue=!1,this.$emit("on-cancel"))}}}),f=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("div",{staticClass:"vux-confirm"},[i("x-dialog",{attrs:{"dialog-class":"android"===t.theme?"weui-dialog weui-skin_android":"weui-dialog","mask-transition":t.maskTransition,"dialog-transition":"android"===t.theme?"vux-fade":t.dialogTransition,"hide-on-blur":t.hideOnBlur,"mask-z-index":t.maskZIndex},on:{"on-hide":function(e){return t.$emit("on-hide")}},model:{value:t.showValue,callback:function(e){t.showValue=e},expression:"showValue"}},[t.title?i("div",{staticClass:"weui-dialog__hd",class:{"with-no-content":!t.showContent}},[i("strong",{staticClass:"weui-dialog__title"},[t._v(t._s(t.title))])]):t._e(),t._v(" "),t.showContent?[t.showInput?i("div",{staticClass:"vux-prompt"},["checkbox"===t.getInputAttrs().type?i("input",t._b({directives:[{name:"model",rawName:"v-model",value:t.msg,expression:"msg"}],ref:"input",staticClass:"vux-prompt-msgbox",attrs:{placeholder:t.placeholder,type:"checkbox"},domProps:{checked:Array.isArray(t.msg)?t._i(t.msg,null)>-1:t.msg},on:{touchend:t.setInputFocus,change:function(e){var i=t.msg,n=e.target,o=!!n.checked;if(Array.isArray(i)){var s=t._i(i,null);n.checked?s<0&&(t.msg=i.concat([null])):s>-1&&(t.msg=i.slice(0,s).concat(i.slice(s+1)))}else t.msg=o}}},"input",t.getInputAttrs(),!1)):"radio"===t.getInputAttrs().type?i("input",t._b({directives:[{name:"model",rawName:"v-model",value:t.msg,expression:"msg"}],ref:"input",staticClass:"vux-prompt-msgbox",attrs:{placeholder:t.placeholder,type:"radio"},domProps:{checked:t._q(t.msg,null)},on:{touchend:t.setInputFocus,change:function(e){t.msg=null}}},"input",t.getInputAttrs(),!1)):i("input",t._b({directives:[{name:"model",rawName:"v-model",value:t.msg,expression:"msg"}],ref:"input",staticClass:"vux-prompt-msgbox",attrs:{placeholder:t.placeholder,type:t.getInputAttrs().type},domProps:{value:t.msg},on:{touchend:t.setInputFocus,input:function(e){e.target.composing||(t.msg=e.target.value)}}},"input",t.getInputAttrs(),!1))]):i("div",{staticClass:"weui-dialog__bd"},[t._t("default",[i("div",{domProps:{innerHTML:t._s(t.content)}})])],2)]:t._e(),t._v(" "),i("div",{staticClass:"weui-dialog__ft"},[t.showCancelButton?i("a",{staticClass:"weui-dialog__btn weui-dialog__btn_default",attrs:{href:"javascript:;"},on:{click:t._onCancel}},[t._v(t._s(t.cancelText||"取消"))]):t._e(),t._v(" "),t.showConfirmButton?i("a",{staticClass:"weui-dialog__btn",class:"weui-dialog__btn_"+t.confirmType,attrs:{href:"javascript:;"},on:{click:t._onConfirm}},[t._v(t._s(t.confirmText||"确定"))]):t._e()])],2)],1)},m=[],p={render:f,staticRenderFns:m},_=p,h=i("VU/8"),v=n,g=h(d,_,!1,v,null,null),w=g.exports,b=(i("YN+o"),i("FWz8")),x=(r.a,l.a,u.a,{components:{Panel:r.a,Group:l.a,Radio:u.a,Confirm:w},watch:{type:function(t,e){this.list=[],this.getData()},userid:function(t,e){}},methods:{onImgError:function(t,e){console.log(t,e)},getData:function(){this.$vux.loading.show({text:"Loading"});var t=this;4!=this.type?Object(b.d)(this.pageindex,6,this.userid,this.type).then(function(e){if(t.$vux.loading.hide(),e.data.length>0){t.pageindex++,t.title="当前拥有 "+e.count+" 条订单数据";for(var i=0;i<e.data.length;i++){var n=e.data[i];t.list.push(t.CrateItem(n))}}else t.$vux.toast.show({text:"没有更多数据了"})}).catch(function(e){console.log(e),t.$vux.loading.hide(),alert("获取超时")}):Object(b.c)(this.pageindex,6,this.points.lat,this.points.lon).then(function(e){if(t.$vux.loading.hide(),e.data.length>0){t.pageindex++,t.title="当前拥有 "+e.count+" 条订单数据";for(var i=0;i<e.data.length;i++){var n=e.data[i];t.list.push(t.CrateItem(n))}}else t.$vux.toast.show({text:"没有更多数据了"})}).catch(function(e){console.log(e),t.$vux.loading.hide(),alert("获取超时")})},CrateItem:function(t){var e={desc:"",meta:{source:"来源信息",date:"时间",other:""},data:null};return e.desc="从"+t.startlocation+"到"+t.endlocation+"的订单.",e.meta.source="价格:"+t.driverprice,e.meta.date=t.startTime.split("+")[0].replace("T"," "),e.data=t,e},onConfirm:function(){this.recOrderFun(this.tmp)},commit:function(t){console.log(t),this.show=!0,this.tmp=t},recOrderFun:function(t){console.log(this.driverInfo);var e={state:4,driverid:this.driverInfo.userid,driverInfo:a()(this.driverInfo)},i=this,n=JSON.parse(t.data.ordersInfo);Object(b.h)(t.data.id,e).then(function(t){if(t){var e="您从"+n.startingPoint+"到"+n.endingPoint+"的订单已经被接收。\r\n司机称呼："+i.driverInfo.usercall+"\r\n联系方式:"+i.driverInfo.mobile,o="您接收从"+n.startingPoint+"到"+n.endingPoint+"的订单已经被接收。\r\n用户称呼："+n.nickname+"\r\n联系方式:"+n.rider+"\r\n时间："+n.StartTime;i.$vux.toast.show({text:"接单成功。"}),Object(b.e)(n.openid,e),console.log("driverInfo",i.driverInfo),Object(b.e)(i.driverInfo.openid,o)}else i.$vux.toast.show({text:"订单已经被其他用户接单。"})})},onfooter:function(){this.getData()}},data:function(){return{list:[],title:"",footer:{title:"获取更多..."},pageindex:1,show:!1,tmp:{}}},created:function(){this.getData()},props:["type","userid","driverInfo","points"]}),C=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("div",{staticClass:"Colorfont",staticStyle:{overflow:"auto",height:"80vh"}},[i("panel",{attrs:{header:t.title,footer:t.footer,list:t.list,type:"4"},on:{"on-click-item":t.commit,"on-click-footer":t.onfooter,"on-img-error":t.onImgError}}),t._v(" "),i("div",[i("confirm",{attrs:{title:"是否接收订单"},on:{"on-confirm":t.onConfirm},model:{value:t.show,callback:function(e){t.show=e},expression:"show"}},[i("p",{staticStyle:{"text-align":"center"}},[t._v("需要接单吗？")])])],1)],1)},y=[],S={render:C,staticRenderFns:y},k=S,T=i("VU/8"),I=o,V=T(x,k,!1,I,"data-v-49f298b1",null);e.default=V.exports},"429B":function(t,e){},FWz8:function(t,e,i){"use strict";function n(t,e,i,n){return d.a.get("/api/orders/getOrder",{params:{userid:i,page:t,limit:e,type:n}})}function o(t,e,i,n){return d.a.get("/api/orders/kclist",{params:{lat:i,lon:n,limit:e,page:t}})}function s(t,e){return d.a.put("/api/orders/"+t,{state:e})}function a(t,e){return d.a.put("/api/orders/"+t,e)}function r(t,e){d.a.post("/api/Token/SendMessage?openid="+t,{message:e})}function l(t,e,i,n){return d.a.get("/api/orders/driverOrder",{params:{page:t,limit:e,userid:i,state:n}})}function u(t){return d.a.put("/orderComplete?id="+t.id,{state:1})}function c(t,e){return d.a.put("/api/orders/meetOrder?id="+t,e)}e.d=n,e.c=o,e.f=s,e.g=a,e.e=r,e.b=l,e.a=u,e.h=c;var d=i("YN+o")},OFgA:function(t,e,i){"use strict";function n(){return Math.random().toString(36).substring(3,8)}e.a={mounted:function(){},data:function(){return{uuid:n()}}}},QHDY:function(t,e,i){"use strict";function n(t,e){for(var i=t.length;i--;)if(t[i]===e)return!0;return!1}function o(t,e){for(var i=t.length;i--;)if(t[i]===e)return!0;return!1}function s(t){i("1Ahr")}var a=i("f6Hi"),r=i("pFYg"),l=i.n(r),u=function(t){return"object"===(void 0===t?"undefined":l()(t))?t.value:t},c=function(t){return"object"===(void 0===t?"undefined":l()(t))?t.key:t},d=function(){var t=arguments.length>0&&void 0!==arguments[0]?arguments[0]:[],e=arguments[1];if(!t.length)return e;if("string"==typeof t[0])return e;var i=t.filter(function(t){return t.key===e});return i.length?i[0].value||i[0].label:e},f=i("Zor0"),m=(a.a,Object(f.a)(),{name:"radio",mixins:[a.a],filters:{getValue:u,getKey:c},props:Object(f.a)(),created:function(){this.handleChangeEvent=!0},methods:{getValue:u,getKey:c,onFocus:function(){this.currentValue=this.fillValue||"",this.isFocus=!0}},watch:{value:function(t){this.currentValue=t},currentValue:function(t){var e=o(this.options,t);""!==t&&e&&(this.fillValue=""),this.$emit("on-change",t,d(this.options,t)),this.$emit("input",t)},fillValue:function(t){this.fillMode&&this.isFocus&&(this.currentValue=t)}},data:function(){return{fillValue:"",isFocus:!1,currentValue:this.value}}}),p=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("div",{staticClass:"weui-cells_radio",class:t.disabled?"vux-radio-disabled":""},[t._l(t.options,function(e,n){return i("label",{staticClass:"weui-cell weui-cell_radio weui-check__label",attrs:{for:"radio_"+t.uuid+"_"+n}},[i("div",{staticClass:"weui-cell__bd"},[t._t("each-item",[i("p",[i("img",{directives:[{name:"show",rawName:"v-show",value:e&&e.icon,expression:"one && one.icon"}],staticClass:"vux-radio-icon",attrs:{src:e.icon}}),t._v(" "),i("span",{staticClass:"vux-radio-label",style:t.currentValue===t.getKey(e)?t.selectedLabelStyle||"":""},[t._v(t._s(t._f("getValue")(e)))])])],{icon:e.icon,label:t.getValue(e),index:n,selected:t.currentValue===t.getKey(e)})],2),t._v(" "),i("div",{staticClass:"weui-cell__ft"},[i("input",{directives:[{name:"model",rawName:"v-model",value:t.currentValue,expression:"currentValue"}],staticClass:"weui-check",attrs:{type:"radio",id:t.disabled?"":"radio_"+t.uuid+"_"+n},domProps:{value:t.getKey(e),checked:t._q(t.currentValue,t.getKey(e))},on:{change:function(i){t.currentValue=t.getKey(e)}}}),t._v(" "),i("span",{staticClass:"weui-icon-checked"})])])}),t._v(" "),i("div",{directives:[{name:"show",rawName:"v-show",value:t.fillMode,expression:"fillMode"}],staticClass:"weui-cell"},[i("div",{staticClass:"weui-cell__hd"},[i("label",{staticClass:"weui-label",attrs:{for:""}},[t._v(t._s(t.fillLabel))])]),t._v(" "),i("div",{staticClass:"weui-cell__bd"},[i("input",{directives:[{name:"model",rawName:"v-model",value:t.fillValue,expression:"fillValue"}],staticClass:"weui-input needsclick",attrs:{type:"text",placeholder:t.fillPlaceholder},domProps:{value:t.fillValue},on:{blur:function(e){t.isFocus=!1},focus:function(e){return t.onFocus()},input:function(e){e.target.composing||(t.fillValue=e.target.value)}}})]),t._v(" "),i("div",{directives:[{name:"show",rawName:"v-show",value:""===t.value&&!t.isFocus,expression:"value==='' && !isFocus"}],staticClass:"weui-cell__ft"},[i("i",{staticClass:"weui-icon-warn"})])])],2)},_=[],h={render:p,staticRenderFns:_},v=h,g=i("VU/8"),w=s,b=g(m,v,!1,w,null,null);e.a=b.exports},"T8/m":function(t,e,i){"use strict";function n(t){i("429B")}var o=i("mvHQ"),s=i.n(o),a=i("0FxO"),r=(String,Object,Array,String,{name:"panel",props:{header:String,footer:Object,list:Array,type:{type:String,default:"1"}},methods:{onImgError:function(t,e){this.$emit("on-img-error",JSON.parse(s()(t)),e),t.fallbackSrc&&(e.target.src=t.fallbackSrc)},getUrl:function(t){return Object(a.a)(t,this.$router)},onClickFooter:function(){this.$emit("on-click-footer"),Object(a.b)(this.footer.url,this.$router)},onClickHeader:function(){this.$emit("on-click-header")},onItemClick:function(t){this.$emit("on-click-item",t),Object(a.b)(t.url,this.$router)}}}),l=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("div",{staticClass:"weui-panel weui-panel_access"},[t.header?i("div",{staticClass:"weui-panel__hd",domProps:{innerHTML:t._s(t.header)},on:{click:t.onClickHeader}},[t._t("header")],2):t._e(),t._v(" "),i("div",{staticClass:"weui-panel__bd"},[t._t("body",["1"===t.type?t._l(t.list,function(e){return i("a",{staticClass:"weui-media-box weui-media-box_appmsg",attrs:{href:t.getUrl(e.url)},on:{click:function(i){return i.preventDefault(),t.onItemClick(e)}}},[e.src?i("div",{staticClass:"weui-media-box__hd"},[i("img",{staticClass:"weui-media-box__thumb",attrs:{src:e.src,alt:""},on:{error:function(i){return t.onImgError(e,i)}}})]):t._e(),t._v(" "),i("div",{staticClass:"weui-media-box__bd"},[i("h4",{staticClass:"weui-media-box__title",domProps:{innerHTML:t._s(e.title)}}),t._v(" "),i("p",{staticClass:"weui-media-box__desc",domProps:{innerHTML:t._s(e.desc)}})])])}):t._e(),t._v(" "),"2"===t.type?t._l(t.list,function(e){return i("div",{staticClass:"weui-media-box weui-media-box_text",on:{click:function(i){return i.preventDefault(),t.onItemClick(e)}}},[i("h4",{staticClass:"weui-media-box__title",domProps:{innerHTML:t._s(e.title)}}),t._v(" "),i("p",{staticClass:"weui-media-box__desc",domProps:{innerHTML:t._s(e.desc)}})])}):t._e(),t._v(" "),"3"===t.type?[i("div",{staticClass:"weui-media-box weui-media-box_small-appmsg"},[i("div",{staticClass:"weui-cells"},t._l(t.list,function(e){return i("a",{staticClass:"weui-cell weui-cell_access",attrs:{href:t.getUrl(e.url)},on:{click:function(i){return i.preventDefault(),t.onItemClick(e)}}},[i("div",{staticClass:"weui-cell__hd"},[i("img",{staticStyle:{width:"20px","margin-right":"5px",display:"block"},attrs:{src:e.src,alt:""},on:{error:function(i){return t.onImgError(e,i)}}})]),t._v(" "),i("div",{staticClass:"weui-cell__bd"},[i("p",{domProps:{innerHTML:t._s(e.title)}})]),t._v(" "),i("span",{staticClass:"weui-cell__ft"})])}),0)])]:t._e(),t._v(" "),"4"===t.type?t._l(t.list,function(e){return i("div",{staticClass:"weui-media-box weui-media-box_text",on:{click:function(i){return i.preventDefault(),t.onItemClick(e)}}},[i("h4",{staticClass:"weui-media-box__title",domProps:{innerHTML:t._s(e.title)}}),t._v(" "),i("p",{staticClass:"weui-media-box__desc",domProps:{innerHTML:t._s(e.desc)}}),t._v(" "),e.meta?i("ul",{staticClass:"weui-media-box__info"},[i("li",{staticClass:"weui-media-box__info__meta",domProps:{innerHTML:t._s(e.meta.source)}}),t._v(" "),i("li",{staticClass:"weui-media-box__info__meta",domProps:{innerHTML:t._s(e.meta.date)}}),t._v(" "),i("li",{staticClass:"weui-media-box__info__meta weui-media-box__info__meta_extra",domProps:{innerHTML:t._s(e.meta.other)}})]):t._e()])}):t._e(),t._v(" "),"5"===t.type?t._l(t.list,function(e){return i("div",{staticClass:"weui-media-box weui-media-box_text",on:{click:function(i){return i.preventDefault(),t.onItemClick(e)}}},[i("div",{staticClass:"weui-media-box_appmsg"},[e.src?i("div",{staticClass:"weui-media-box__hd"},[i("img",{staticClass:"weui-media-box__thumb",attrs:{src:e.src,alt:""},on:{error:function(i){return t.onImgError(e,i)}}})]):t._e(),t._v(" "),i("div",{staticClass:"weui-media-box__bd"},[i("h4",{staticClass:"weui-media-box__title",domProps:{innerHTML:t._s(e.title)}}),t._v(" "),i("p",{staticClass:"weui-media-box__desc",domProps:{innerHTML:t._s(e.desc)}})])]),t._v(" "),e.meta?i("ul",{staticClass:"weui-media-box__info"},[i("li",{staticClass:"weui-media-box__info__meta",domProps:{innerHTML:t._s(e.meta.source)}}),t._v(" "),i("li",{staticClass:"weui-media-box__info__meta",domProps:{innerHTML:t._s(e.meta.date)}}),t._v(" "),i("li",{staticClass:"weui-media-box__info__meta weui-media-box__info__meta_extra",domProps:{innerHTML:t._s(e.meta.other)}})]):t._e()])}):t._e()])],2),t._v(" "),i("div",{staticClass:"weui-panel__ft"},[t.footer&&t.footer.title&&"3"!==t.type?i("a",{staticClass:"weui-cell weui-cell_access weui-cell_link",attrs:{href:t.getUrl(t.footer.url)},on:{click:function(e){return e.preventDefault(),t.onClickFooter(e)}}},[i("div",{staticClass:"weui-cell__bd",domProps:{innerHTML:t._s(t.footer.title)}})]):t._e()])])},u=[],c={render:l,staticRenderFns:u},d=c,f=i("VU/8"),m=n,p=f(r,d,!1,m,null,null);e.a=p.exports},Zor0:function(t,e,i){"use strict";e.a=function(){return{options:{type:Array,required:!0},value:[String,Number],fillMode:{type:Boolean,default:!1},fillPlaceholder:{type:String,default:"其他"},fillLabel:{type:String,default:"其他"},disabled:Boolean,selectedLabelStyle:Object}}},f6Hi:function(t,e,i){"use strict";var n=i("OFgA");e.a={mixins:[n.a],props:{required:{type:Boolean,default:!1}},created:function(){this.handleChangeEvent=!1},computed:{dirty:{get:function(){return!this.pristine},set:function(t){this.pristine=!t}},invalid:function(){return!this.valid}},methods:{setTouched:function(){this.touched=!0}},watch:{value:function(t){!0===this.pristine&&(this.pristine=!1),this.handleChangeEvent||(this.$emit("on-change",t),this.$emit("input",t))}},data:function(){return{errors:{},pristine:!0,touched:!1}}}},nnSp:function(t,e){},qHQD:function(t,e){},rHil:function(t,e,i){"use strict";function n(t){i("qHQD")}var o=i("wmxo"),s=(o.a,String,String,String,String,String,String,Number,String,String,{name:"group",methods:{cleanStyle:o.a},props:{title:String,titleColor:String,labelWidth:String,labelAlign:String,labelMarginRight:String,gutter:[String,Number],footerTitle:String,footerTitleColor:String}}),a=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("div",[t.title?i("div",{staticClass:"weui-cells__title",style:t.cleanStyle({color:t.titleColor}),domProps:{innerHTML:t._s(t.title)}}):t._e(),t._v(" "),t._t("title"),t._v(" "),i("div",{staticClass:"weui-cells",class:{"vux-no-group-title":!t.title},style:t.cleanStyle({marginTop:"number"==typeof t.gutter?t.gutter+"px":t.gutter})},[t._t("after-title"),t._v(" "),t._t("default")],2),t._v(" "),t.footerTitle?i("div",{staticClass:"weui-cells__title vux-group-footer-title",style:t.cleanStyle({color:t.footerTitleColor}),domProps:{innerHTML:t._s(t.footerTitle)}}):t._e()],2)},r=[],l={render:a,staticRenderFns:r},u=l,c=i("VU/8"),d=n,f=c(s,u,!1,d,null,null);e.a=f.exports},wmxo:function(t,e,i){"use strict";e.a=function(){var t=arguments.length>0&&void 0!==arguments[0]?arguments[0]:{};for(var e in t)void 0===t[e]&&delete t[e];return t}},"y0+N":function(t,e){}});
//# sourceMappingURL=4.f265f702125ac06b5ec1.js.map