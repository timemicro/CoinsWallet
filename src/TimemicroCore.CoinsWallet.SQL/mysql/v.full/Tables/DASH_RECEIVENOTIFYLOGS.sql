DROP TABLE IF EXISTS DASH_RECEIVENOTIFYLOGS;

CREATE TABLE DASH_RECEIVENOTIFYLOGS
(
  ID BIGINT AUTO_INCREMENT PRIMARY KEY,
  TXID NVARCHAR(64) NOT NULL,
  ADDRESS NVARCHAR(52),
  AMOUNT DECIMAL(16, 8) DEFAULT 0,
  NOTIFIEDCOUNT INT DEFAULT 0,
  NOTIFYRESPONSETEXT TEXT,
  NEXTNOTIFYTIME DATETIME  DEFAULT CURRENT_TIMESTAMP(),
  CREATETIME DATETIME DEFAULT CURRENT_TIMESTAMP()
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

ALTER TABLE DASH_RECEIVENOTIFYLOGS ADD INDEX IX_DASH_RECEIVENOTIFYLOGS_NEXTNOTIFYTIME(NEXTNOTIFYTIME);
