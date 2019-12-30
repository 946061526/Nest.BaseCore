/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50726
Source Host           : localhost:3306
Source Database       : db1

Target Server Type    : MYSQL
Target Server Version : 50726
File Encoding         : 65001

Date: 2019-12-30 17:45:47
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for appticket
-- ----------------------------
DROP TABLE IF EXISTS `appticket`;
CREATE TABLE `appticket` (
  `Id` varchar(36) NOT NULL,
  `AppId` varchar(50) NOT NULL,
  `ClientType` varchar(20) NOT NULL,
  `DeviceNo` varchar(30) NOT NULL,
  `Noncestr` varchar(50) DEFAULT NULL,
  `AppSecret` varchar(50) NOT NULL,
  `Ticket` varchar(50) NOT NULL,
  `LastUpdateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of appticket
-- ----------------------------

-- ----------------------------
-- Table structure for menu
-- ----------------------------
DROP TABLE IF EXISTS `menu`;
CREATE TABLE `menu` (
  `id` varchar(50) NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  `parentId` int(11) DEFAULT NULL,
  `path` varchar(255) DEFAULT NULL,
  `icon` varchar(255) DEFAULT NULL,
  `type` int(4) NOT NULL DEFAULT '1' COMMENT '类型：1模块 2功能 3操作',
  `sort` int(4) NOT NULL DEFAULT '0' COMMENT '排序',
  `hidden` bit(1) NOT NULL,
  PRIMARY KEY (`id`,`type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of menu
-- ----------------------------

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role` (
  `id` varchar(50) NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of role
-- ----------------------------

-- ----------------------------
-- Table structure for rolemenu
-- ----------------------------
DROP TABLE IF EXISTS `rolemenu`;
CREATE TABLE `rolemenu` (
  `id` varchar(50) NOT NULL,
  `roleId` varchar(50) DEFAULT NULL,
  `menuId` varchar(50) DEFAULT NULL,
  `readed` bit(1) NOT NULL,
  `writed` bit(1) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of rolemenu
-- ----------------------------

-- ----------------------------
-- Table structure for userinfo
-- ----------------------------
DROP TABLE IF EXISTS `userinfo`;
CREATE TABLE `userinfo` (
  `Id` varchar(36) NOT NULL,
  `UserName` varchar(20) NOT NULL,
  `Pwd` varchar(50) NOT NULL,
  `RealName` varchar(20) NOT NULL,
  `Status` int(11) NOT NULL DEFAULT '1',
  `CreateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of userinfo
-- ----------------------------
