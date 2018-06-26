<?php 
include ('Functions.php');
ini_set("default_charset","utf-8");


print_r($_POST);
echo "<br>";
print_r($_GET);
echo "<br>";
if(isset($_POST['username'])) {
echo count($_POST['username'];
}
if(isset($_GET['register']) && isset($_POST)) {
	$hibak = array();
	if((!isset($_POST['username'])) || $_POST['username'] =="" || count($_POST['username']) < 4){
		$hibak[] = "A felhasználónév megadása kötelező! Legalább 4 karakter legyen!"; 
	}
	
	if(!isset($_POST['fullname']) || $_POST['fullname'] == "") {
		$hibak[] = "A teljsen név megadása kötelező! Csak nagybetűből állhat!";
	}
	
	if(!isset($_POST['email']) || $_POST['email'] == "") {
		$hibak[] = "Az email cím megadása kötlező! Érvényes email címet adj meg!";
	}
	
	if(!isset($_POST['pwd']) || count($_POST['pwd']) < 5) {
		$hibak[] = "A jelszó megadása kötelező! A jelszó hossza legyen legalább 5.";
	}
	
	
	
	if(!hibak){
		$ujTag=array( //elkészítjük a bejegyzést az elküldött adatokból
			"username"=>trim($_POST['username']),
			"fullname"=>$_POST['fullname'],
			"email"=>$_POST['email'],
			"pwd"=>md5($_POST['pwd']),
		);
		$tagok = json_decode(file_get_contents('regadat.json'),true);
		$tagok[]=$ujTag; 
		file_put_contents('regadat.json',json_encode($tagok));
		echo "siker";
	} else {
		foreach( $hibak as $hiba) {
			echo $hiba . "<br>";
		}
	}

}
echo "vége";



/*




echo "check user";

$rendeles=array( //elkészítjük a rendelést az elküldött adatokból
			"vezeteknev"=>$_POST['vezeteknev'],
			"keresztnev"=>$_POST['keresztnev'],
			"telefonszam"=>$_POST['telefonszam'],
			"email"=>$_POST['email'],
			"szallitasi"=>$_POST['szallitasi'],
			"termekek"=>$_SESSION['kosar'] //a rendelés tartalma a kosár aktuális tartalma lesz
		);
$rendelesek=json_decode(file_get_contents('rendelesek.json'),true); //beolvassuk az eddigi rendeléseket
		$rendelesek[]=$rendeles; //hozzáadunk egy új rendelést
		file_put_contents('rendelesek.json',json_encode($rendelesek)); //visszaírjuk a módosított rendelések listát

function betolt_adat($fajlnev, $adat = array()) {
	$s = @file_get_contents($fajlnev);
	return $s==false 
		? $adat
		: json_decode($s, true);
}

function elment_adat($fajlnev, $adat) {
	$s = json_encode($adat);
	file_put_contents($fajlnev, $s, LOCK_EX);
}
$hibak = array();


	
	$posted['user'] = trim($_POST['user']);
	$posted['pwd'] = $_POST['pwd'];
	$posted['fullname'] = trim($_POST['fullname']);
	$posted['email'] = $_POST['email'];	
	$data = betolt_adat("regadat.txt", array());
	array_push($posted,$data);
	elment_adat("regadat.txt", $data);
	echo "DONE";
	*/
	
	/*if (array_key_exists($felhnev)) {
	
	
	
		if ($felhnev == '') {
			$hibak[] = 'Felhasználónév kötelező!';
		}
		if ($logjelszo == '') {
			$hibak[] = 'Jelszó kötelező!';
		}
		if (!(array_key_exists($felhnev, $tagok) &&
			$tagok[$felhnev]['logjelszo'] == md5($logjelszo))) {
			$hibak[] = 'Hibas bejelentkezesi adatok!';
		}
		------------------------------------------------------------------------------
		if (!$hibak) {
			$_SESSION['azonositott'] = true;
			$_SESSION['felhnev'] = $felhnev;
			$_SESSION['nev'] = $tagok[$felhnev]['nev'];
			$_SESSION['cim'] = $tagok[$felhnev]['cim'];
			}
		------------------------------------------------------------------------------------------
	}*/


?>