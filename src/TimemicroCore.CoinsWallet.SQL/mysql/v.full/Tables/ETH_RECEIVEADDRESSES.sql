DROP TABLE IF EXISTS ETH_RECEIVEADDRESSES;

CREATE TABLE ETH_RECEIVEADDRESSES
(
  ADDRESS NVARCHAR(45) PRIMARY KEY NOT NULL,
  PRIVATEKEY NVARCHAR(52),
  TOTALRECEIVED DECIMAL(16, 8) DEFAULT 0,
  CREATETIME DATETIME DEFAULT CURRENT_TIMESTAMP()
)ENGINE=InnoDB DEFAULT CHARSET=utf8;

