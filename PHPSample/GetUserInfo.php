<?php

$id = 0;

//今回はポストパラメーターがなかった場合プログラムを終了する
if(!isset($_POST["id"]))
{
    echo "POST_PARAM_IS_EMPTY";
    exit();
}

$id = $_POST["id"];

//データベースに接続するためのオブジェクト
$mysql = new PDO('mysql:dbname=unitydb;host=localhost;charset=utf8','root',"",
                [PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
                PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,]
);

//SQL文の組み立て
$query = "select * from user where id = :id";

//ステートメントオブジェクトの作成
$steatment = $mysql->prepare($query);

//ステートメントにデータを設定
$steatment->bindValue(':id',$id);

//SQL文の発行
$steatment->execute();

//SQL文の結果の取得
$result = $steatment->fetch(PDO::FETCH_ASSOC);


if($result == null)
{
    echo "DONT_GET_DATA";
    exit();
}

//配列の作成
$json = array('_id' => $result['id'],
              '_name' => $result['name'],
              '_castumData' => $result['castumdata']);

//配列のjson化
echo json_encode($json);


?>