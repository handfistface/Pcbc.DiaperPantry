# MySql 

Here's a useful [guide](https://ubuntu.com/server/docs/install-and-configure-a-mysql-server) to configuring mysql server
Ip: 192.168.0.35
Port: 33060, 3306
Username: john
Username: root
Everything should be done through my john user
Run the following command to get the root password ```sudo cat /etc/mysql/debian.cnf```

Incite the interactive sql with the following command ```sudo mysql -u root```
I was able to create the john user with the following commands.
```
CREATE USER 'john'@'localhost' IDENTIFIED BY 'set-password';
GRANT ALL PRIVILEGES ON *.* TO 'john'@'localhost' WITH GRANT OPTION;
CREATE USER 'john'@'%' IDENTIFIED BY 'set-password';
GRANT ALL PRIVILEGES ON *.* TO 'john'@'%' WITH GRANT OPTION;

GRANT GRANT OPTION ON *.* TO 'john'@'%';
GRANT GRANT OPTION ON *.* TO 'john'@'localhost';
GRANT ALL PRIVILEGES ON *.* TO 'john'@'192.168.0.*';
FLUSH PRIVILEGES;
```

## Networking & Firewall 
I have a firewall at some point, but can launch the server and login now
I was able to turn off the firewall with the following command ```sudo ufw allow mysql```. 
You could also manually allow ports 3306 & 33060 with the same command, just substitute mysql with the ports & tcp/udp

You also need to get mysql to bind to the correct adaptor. You do so by editing the following 
file ```/etc/mysql/mysql.conf.d/mysqld.cnf```. Note that there's also a mysql.cnf, this is not the one
Edit the line that has the bind-address to ```bind-address = 0.0.0.0```. 
Restart the mysql server & mysqld config service 
```
sudo systemctl restart mysql
sudo mysqld restart
```
To make sure you're running mysql on the correct ports & adaptor, run the following command: ```sudo netstat -tulnp | grep mysql```

