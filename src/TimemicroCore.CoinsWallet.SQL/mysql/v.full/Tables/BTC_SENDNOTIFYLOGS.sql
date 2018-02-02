DROP TABLE IF EXISTS BTC_SENDNOTIFYLOGS;

CREATE TABLE BTC_SENDNOTIFYLOGS
(
  ID BIGINT AUTO_INCREMENT PRIMARY KEY,
  TXID NVARCHAR(64) NOT NULL,
  ADDRESS NVARCHAR(34),
  NOTIFIEDCOUNT INT DEFAULT 0,
  NOTIFYRESPONSETEXT TEXT,
  NEXTNOTIFYTIME DATETIME  DEFAULT CURRENT_TIMESTAMP(),
  CREATETIME DATETIME DEFAULT CURRENT_TIMESTAMP()
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

ALTER TABLE BTC_SENDNOTIFYLOGS ADD INDEX IX_BTC_SENDNOTIFYLOGS_NEXTNOTIFYTIME(NEXTNOTIFYTIME);