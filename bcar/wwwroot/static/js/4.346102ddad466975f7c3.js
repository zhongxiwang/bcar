webpackJsonp([4],{"4jai":function(t,e,a){var s=a("EULx");"string"==typeof s&&(s=[[t.i,s,""]]),s.locals&&(t.exports=s.locals);a("rjj0")("7a722de1",s,!0,{})},EULx:function(t,e,a){e=t.exports=a("FZ+f")(!0),e.push([t.i,"\n.popup1[data-v-0137cb24] {\n  width: 100%;\n  height: 100%;\n}\n","",{version:3,sources:["E:/Car/view/driver/src/view/setting/orderlist.vue"],names:[],mappings:";AACA;EACE,YAAY;EACZ,aAAa;CACd",file:"orderlist.vue",sourcesContent:["\n.popup1[data-v-0137cb24] {\n  width: 100%;\n  height: 100%;\n}\n"],sourceRoot:""}])},nvey:function(t,e,a){"use strict";function s(t){a("4jai")}Object.defineProperty(e,"__esModule",{value:!0});var i=a("T8/m"),o=a("rHil"),n=a("QHDY"),r=a("ABCa"),l=a("HD9R"),d=(a("YN+o"),a("FWz8")),u=a("32ER"),c=a("1DHf"),h=a("jHVj"),p=a("2sLL"),v=(Object,Number,i.a,o.a,n.a,r.a,u.a,c.a,h.a,p.a,{props:{dataset:Object,type:Number},data:function(){return{options:[],val:"",riderType:["","顺风车","装车","速递","快车"],mobile:"",url:"tel://",stateEnum:["取消","待出行","完成交易","正在进行","过期","已接单"]}},computed:{},created:function(){},mounted:function(){},watch:{dataset:function(t,e){null!=this.dataset&&this.getData()}},methods:{getData:function(){void 0!=this.dataset.data&&(this.options=[],1==this.type?(this.ordersInfo(),this.val=this.dataset.data.driverprice):2==this.type&&(this.ordersInfo(),this.driverInfo(),this.val=this.dataset.data.userprice),this.options.push({label:"订单状态",value:this.stateEnum[this.dataset.data.state+1]}))},dateFormat:function(t){var e=new Date(t);return null==t&&(e=new Date),e.getFullYear()+"-"+(e.getMonth()+1<10?"0"+(e.getMonth()+1):e.getMonth()+1)+"-"+(e.getDate()<10?"0"+e.getDate():e.getDate())+" "+(e.getHours()<10?"0"+e.getHours():e.getHours())+":"+(e.getMinutes()<10?"0"+e.getMinutes():e.getMinutes())+":"+(e.getSeconds()<10?"0"+e.getSeconds():e.getSeconds())},ordersInfo:function(){var t=this.dataset.data.ordersInfo;void 0!=t&&null!=t&&""!=t&&(t=JSON.parse(t),this.mobile=t.rider,this.url="tel://"+this.mobile,this.options.push({label:"用户称呼",value:t.nickname}),this.options.push({label:"联系方式",value:t.rider}),this.options.push({label:"乘车时间",value:this.dateFormat(t.startDate)}),this.options.push({label:"上车地址",value:t.startingPoint}),this.options.push({label:"下车地址",value:t.endingPoint}),this.options.push({label:"订单类型",value:this.riderType[t.riderType]}),3==t.riderType&&(this.options.push({label:"收件人称呼",value:t.addresseeCall}),this.options.push({label:"收件人联系方式",value:t.addresseeMobile})),1!=t.riderType&&2!=t.riderType||this.options.push({label:"乘车人数",value:t.personNum}))},driverInfo:function(){var t=this.dataset.data.driverInfo;void 0!=t&&null!=t&&""!=t&&(t=JSON.parse(t),this.mobile=t.mobile,this.url="tel://"+this.mobile,this.options.push({label:"司机称呼",value:t.usercall}),this.options.push({label:"车牌号",value:t.carcard}),this.options.push({label:"联系方式",value:t.mobile}),this.options.push({label:"车身颜色",value:t.carColor}))},call:function(){window.location.href=this.url},jd:function(){var t=this,e=JSON.parse(this.dataset.data.driverInfo),a=JSON.parse(this.dataset.data.ordersInfo);console.log(a),Object(d.f)(this.dataset.data.id,{state:"2",arrivalTime:this.dateFormat()}).then(function(s){if(console.log(s),s){t.$vux.toast.show({text:"开始接单，5分钟后乘客没到达可以取消"});var i="您的订单，司机已经到达接车地点，5分钟后乘客没到达司机可以免费取消，请注意。\r\n车牌号："+e.carcard+"\r\n司机称呼："+e.usercall+"\r\n联系方式:"+e.mobile;Object(d.d)(a.openid,i)}else alert("接单失败")})},desc:function(){var t=this,e=JSON.parse(this.dataset.data.ordersInfo.openid),a=JSON.parse(this.dataset.data);Object(d.e)(this.dataset.data.id,"-1").then(function(s){if(s){t.$vux.toast.show({text:"订单已经被取消"});var i="您从"+a.startingPoint+"到"+a.endingPoint+"的订单已经被取消。";Object(d.d)(e,i)}else alert("接单失败")})},complate:function(){var t=this,e=this.dataset.data.ordersInfo.openid,a=this.dataset.data;Object(d.a)(this.dataset.data).then(function(s){if(console.log(s),s){t.$vux.toast.show({text:"订单已经完成，等待用户确认"}),t.data.state=1;var i="您从"+a.startingPoint+"到"+a.endingPoint+"的订单已经完成。";Object(d.d)(e,i)}else t.$vux.toast.show({text:"订单提交失败"})})}},components:{Panel:i.a,Group:o.a,Radio:n.a,XHeader:r.a,CellBox:u.a,Cell:c.a,CellFormPreview:h.a,XButton:p.a}}),f=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",[a("group",[a("cell",{attrs:{title:"订单信息"}},[t._v(t._s(t.val)+"元")]),t._v(" "),a("cell-form-preview",{attrs:{list:t.options}})],1),t._v(" "),a("br"),t._v(" "),a("x-button",{attrs:{type:"primary"},nativeOn:{click:function(e){return t.call(e)}}},[t._v("呼叫 "+t._s(t.mobile))]),t._v(" "),4==t.dataset.data.state?a("x-button",{attrs:{type:"primary"},nativeOn:{click:function(e){return t.jd(e)}}},[t._v("到达接送地点")]):t._e(),t._v(" "),2==t.dataset.data.state?a("x-button",{attrs:{type:"primary"},nativeOn:{click:function(e){return t.complate(e)}}},[t._v("完成订单")]):t._e(),t._v(" "),2==t.dataset.data.state?a("x-button",{attrs:{type:"primary"},nativeOn:{click:function(e){return t.complate(e)}}},[t._v("取消订单")]):t._e()],1)},m=[],g={render:f,staticRenderFns:m},b=g,x=a("VU/8"),y=x(v,b,!1,null,null,null),w=y.exports,_=(i.a,o.a,n.a,r.a,l.a,{components:{Panel:i.a,Group:o.a,Radio:n.a,XHeader:r.a,Popup:l.a,orderInfo:w},watch:{},methods:{onImgError:function(t,e){console.log(t,e)},getData:function(){this.$vux.loading.show({text:"Loading"});var t=this;Object(d.b)(this.pageindex,6,this.userid,this.state).then(function(e){if(t.$vux.loading.hide(),e.data.length>0){t.pageindex++,t.title="当前拥有 "+e.count+" 条订单数据";for(var a=0;a<e.data.length;a++){var s=e.data[a];t.list.push(t.CrateItem(s))}}else t.$vux.toast.show({text:"没有更多数据了"})}).catch(function(e){console.log(e),t.$vux.loading.hide(),alert("获取超时")})},CrateItem:function(t){var e={desc:"",meta:{source:"来源信息",date:"时间",other:""},data:null};return e.desc="从"+t.startlocation+"到"+t.endlocation+"的订单.",e.meta.source="价格:"+t.driverprice,e.meta.date=t.startTime.split("+")[0].replace("T"," "),e.data=t,e.meta.other=t.state,e},commit:function(t){console.log(t),this.dataset=t,this.show=!0},onfooter:function(){this.getData()},ClosePopup:function(){this.show=!1}},data:function(){return{list:[],title:"",footer:{title:"获取更多..."},pageindex:0,state:"",userid:"",type:"4",show:!1,dataset:null,isDriver:1}},created:function(){this.userid=this.$route.query.userid,this.state=this.$route.query.state,this.isDriver=parseInt(this.$route.query.isDriver),this.getData()}}),C=function(){var t=this,e=t.$createElement,a=t._self._c||e;return a("div",{staticClass:"Colorfont"},[a("x-header",{attrs:{title:"订单列表"}}),t._v(" "),a("panel",{staticStyle:{overflow:"auto",height:"90vh"},attrs:{header:t.title,footer:t.footer,list:t.list,type:t.type},on:{"on-click-item":t.commit,"on-click-footer":t.onfooter,"on-img-error":t.onImgError}}),t._v(" "),a("popup",{attrs:{height:"100%"},model:{value:t.show,callback:function(e){t.show=e},expression:"show"}},[a("x-header",{attrs:{"left-options":{preventGoBack:!0},title:"订单信息"},on:{"on-click-back":t.ClosePopup}}),t._v(" "),a("div",{staticClass:"popup1"},[a("orderInfo",{attrs:{dataset:t.dataset,type:t.isDriver}})],1)],1)],1)},O=[],j={render:C,staticRenderFns:O},D=j,I=a("VU/8"),E=s,P=I(_,D,!1,E,"data-v-0137cb24",null);e.default=P.exports}});
//# sourceMappingURL=4.346102ddad466975f7c3.js.map