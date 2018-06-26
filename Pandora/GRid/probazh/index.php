<?php
	session_start(); //session beállítása
	header('Content-type: text/html; Charset=utf-8'); //utf-8 kódolás
	if(!isset($_SESSION['kosar'])){ //ha nem volt még kosarunk, létrehozzuk
		$_SESSION['kosar']=array();
	}
	$termekek_eredeti=array( //ebben a változóban tároljuk az összes terméket, de akár fájlból is beolvashatnánk
		array(
			"nev"=>"Zokni",
			"kategoria"=>"Ruházat",
			"ar"=>1200
		),
		array(
			"nev"=>"Nadrág",
			"kategoria"=>"Ruházat",
			"ar"=>8000
		),
		array(
			"nev"=>"Paradicsom konzerv",
			"kategoria"=>"Élelmiszer",
			"ar"=>115
		)
	
	);
	
	if(isset($_POST['db'])){ //hozzáadtunk egy terméket a kosárhoz
		$_SESSION['kosar'][]=array( //[] operátor: push
			"nev"=>$_POST["nev"],
			"db"=>$_POST["db"]
		);
	}
	
	if(isset($_POST['vezeteknev'])){ //rendelés véglegesítése
		
		$rendeles=array( //elkészítjük a rendelést az elküldött adatokból
			"vezeteknev"=>$_POST['vezeteknev'],
			"keresztnev"=>$_POST['keresztnev'],
			"telefonszam"=>$_POST['telefonszam'],
			"email"=>$_POST['email'],
			"szallitasi"=>$_POST['szallitasi'],
			"termekek"=>$_SESSION['kosar'] //a rendelés tartalma a kosár aktuális tartalma lesz
		);
		if($_POST['megegyezik']=='on'){ //ha megegyezik a két cím, átmásoljuk
			$rendeles['szamlazasi']=$rendeles['szallitasi'];
		}else{
			$rendeles['szamlazasi']=$_POST['szamlazasi'];
		}
		
		$rendelesek=json_decode(file_get_contents('rendelesek.json'),true); //beolvassuk az eddigi rendeléseket
		$rendelesek[]=$rendeles; //hozzáadunk egy új rendelést
		file_put_contents('rendelesek.json',json_encode($rendelesek)); //visszaírjuk a módosított rendelések listát
		
		$_SESSION['kosar']=array(); //kosár kiürítése

		echo 'A rendelését felvettük!'; //igazából úgy lenne szép, ha egy változóban tárolnánk a megjeleníteni kívánt üzenetet, és azt a sablonban a megfelelő helyen kiírnánk
	}
	
	if(isset($_POST['kategoria'])){ //kategória szűrés
		$termekek=array(); //megjelenítendő termékek
		foreach($termekek_eredeti as $termek){ //csak azt rakjuk bele a termekek tömbbe, amelyiknek stimmel a kategóriája
			if($termek["kategoria"]==$_POST["kategoria"]){
				$termekek[]=$termek;
			}
		}
	}else{
		$termekek=$termekek_eredeti; //ha nincs szűrés, akkor az összeset megjelenítjük
	}

	include 'termekek.html'; //megfelelő sablon betöltése (a kiterjesztés lehet bármi, nem csak html, de ez jelzi, hogy itt elsősorban nem php kódot írunk már)

?>