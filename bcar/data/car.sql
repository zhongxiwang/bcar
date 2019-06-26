 -- 创建司机信息
create table driverinfo(
	userid int not null,
	username varchar(32) not null,
	usercall varchar(16) not null,
	carcard varchar(20) not null,
	route varchar(48) not null,
	carType varchar(36) not null,
	seatNum int not null,
	carColor varchar(10) not null,
	moblie varchar(11) ,
	idcard varchar(16) not null,
	driverlicense varchar(16) not null,
	drivinglicense varchar(16) not null,
	carimage varchar(16) not null,
	driverstate int default 0,
	operationcertificate varchar(16),
	createtime TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
 
-- 用户信息,proxy 为1 表示是代理商
create table userinfo(
	id int not null AUTO_INCREMENT primary key,
	username varchar(32) not null,
	mobile varchar(11),
	wxCount varchar(64) not null,
	bill float default 0,
	recommender varchar(64) not null,
	userid int,
	qrCode varchar(256) not null,
	headimgurl varchar(256) not null,
	createtime TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	proxy int default 0
);

-- state 0 表示为待出行，-1 表示取消，1 表示完成交易， 2 表示正在进行
create table userbill(
	userid int not null,
	id int not null AUTO_INCREMENT primary key,
	driverid int not null,
	driverInfo VARCHAR(400) not null,
	startlocation varchar(128) not null,
	endlocation varchar(128) not null,
	actualprice float not null,
	state int default 0,
	createtime TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


create table driverbill(
	userid int not null,
	id int not null AUTO_INCREMENT,
	userid int not null,
	endlocation varchar(128) not null,
	actualprice int not null,
	state int default 0
);


create table orders(
	userid int not null,
	id int not null AUTO_INCREMENT primary key,
	driverid int ,
	driverInfo VARCHAR(400) ,
	startlocation varchar(128) not null,
	endlocation varchar(128) not null,
	state int default 0,
	userprice float default 0,
	driverprice float default 0,
	arrivalTime TIMESTAMP,
	ordersInfo varchar(1200) not null,
	orderType int not null,
	createtime TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	routeid int not null,
	StartTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 消息类型默认是分享收益消息
create table message(
 recuser int not null,
 senduser int not null,
 message varchar(512),
 messageType int default 0,
 createtime TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


create table configuration(
	keyv varchar(36) not null,
	valuev varchar(100) not null,
	description varchar(255) not null
);

-- file upload
create table upload(
	id varchar(16) not null,
	filename varchar(128) not null,
	uptime TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
-- - 分享数据表,0 用户用车提取收益，1 司机收益提取，2 代理商收益提取，3 提现
create table sharebill(
	id int not null AUTO_INCREMENT primary key,
	userid int not null,
	username varchar(32) not null,
	createtime TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	price float not null,
	billid int,
	opertype int default 0,
	wxcount varchar(64) not null
);

create table iroute(
	id int not null AUTO_INCREMENT primary key,
	startLocation varchar(128) not null,
	endLocation varchar(128) not null
);

create table locationSite(
	id int not null AUTO_INCREMENT primary key,
	irouteid int not null,
	sitename varchar(128),
	nxdistance int,
	lindex int not null
);
create table adminTable(
	id int not null AUTO_INCREMENT primary key,
	username varchar(21) not null,
	pwd varchar(64),
	email varchar(64)
);
