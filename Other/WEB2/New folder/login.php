<?php
include('check.php');
print_r($_POST);

//------------------------------------------------------------------------------

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
//---------------------------------------------------------------------------
$hibak = array();
$reghibak = array();

if ($_POST) {

	$felhnev = trim($_POST['felhnev']);
	$logjelszo = $_POST['logjelszo'];
	$tagok = betolt_adat('regadat.txt');
	$usernev = trim($_POST['usernev']);
	$nev = trim($_POST['nev']);
	$jelszo = $_POST['jelszo'];
	$rejelszo = $_POST['rejelszo'];
	$cim = trim($_POST['cim']);
	
	
	
	if (array_key_exists($felhnev)) {
	
	
	
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
		//------------------------------------------------------------------------------
		if (!$hibak) {
			$_SESSION['azonositott'] = true;
			$_SESSION['felhnev'] = $felhnev;
			$_SESSION['nev'] = $tagok[$felhnev]['nev'];
			$_SESSION['cim'] = $tagok[$felhnev]['cim'];
			
			header('Location: check.php');
			exit();

			}
		//------------------------------------------------------------------------------------------
	}else{
	
	
	
			
			if ($usernev == '') {
				$reghibak[] = 'Felhasználó név megadása kötelező!';
			}
			if ( ($jelszo == '') || ($rejelszo == '') ) {
				$reghibak[] = 'Mindkét jelszó megadásáa kötelező!';
			}
			if ($nev == '') {
				$reghibak[] = 'Név megadása kötelező!';
			}
			if ($cim == '') {
				$reghibak[] = 'E-mail cim megadása kötelező!';
			}
			if (!($jelszo == $rejelszo)) {
				$reghibak[] = 'A két jelszó nem egyezik!';
			}
			if (array_key_exists($usernev, $tagok)) {
				$reghibak[] = 'Mar letezo felhasználó név!';
			}
			
			//---------------------------------------------------------------------
			if (!$reghibak) {
				$tagok[$usernev] = array(
					'jelszo'		=> md5($jelszo),
					'nev'				=> $nev,
					'cim'				=> $cim,
				);
				elment_adat('regadat.txt', $tagok);
				header('Location: login.php');
				exit();
				
			}
			//--------------------------------------------------------------------
		}
	}
?>
