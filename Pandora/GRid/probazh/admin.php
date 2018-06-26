<?php
	header('Content-type: text/html; Charset=utf-8'); //utf-8 kódolás

	$rendelesek=json_decode(file_get_contents('rendelesek.json'),true); //rendelések betöltése

	if(isset($_POST['id'])){ //törlés
		array_splice($rendelesek, $_POST['id'],1); //tömbből az adott elem törlése úgy, hogy a mögötte levőket a helyére csúsztatjuk
		file_put_contents('rendelesek.json',json_encode($rendelesek)); //visszaírjuk a fájlba a törölt rendelést már nem tartalmazó listát
	}
	
	include 'rendelesek.html'; //rendelések megjelenítése

?>