<?php
    $id = 0;
    $name = "";
    $castumdata = "";

    if(!isset($_POST['id']) || !isset($_POST['id']) ||!isset($_POST['id']))
    {
        echo "NOT_SET_POST_DATA";
        exit;
    }

    $id = $_POST['id'];
    $name = $_POST['name'];
    $castumdata = $_POST['castumdata'];

    $mysql = new PDO('mysql:dbname=unitydb;host=localhost;charset=utf8','root',"",
    [PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
    PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,]
);

    $query = "update user set name = :name,castumdata = :castumdata where id = :id";

    $statment = $mysql->prepare($query);
    $statment->bindValue(':name',$name);
    $statment->bindValue(':castumdata',$castumdata);
    $statment->bindValue(':id',$id);

  if(!$statment->execute())
  {
      echo "FAILD_QUERY";
      exit();
  }

  echo "UPDATE_CONPLEAT";


?>