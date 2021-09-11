<?php

    $mysql =new PDO('mysql:dbname=unitydb;host=localhost;charset=utf8','root',"",
    [PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
    PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,]
);

//最新のユーザーのidを取得
    $query = "select id from user order by id desc limit 1";
    $result = $mysql->query($query);

    $statement = $result->fetch(PDO::FETCH_ASSOC);
    if($statement == null)
    {
        echo "FAILD_GET_LAST_ID";
        exit();
    }

    echo $statement;
?>