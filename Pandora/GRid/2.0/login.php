<?php
echo "Helooe";
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

if ($_POST) {

	$felhnev = trim($_POST['user']);
	$logjelszo = $_POST['pwd'];
	$tagok = betolt_adat('regadat.txt');
	
	
	
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
			}
		//------------------------------------------------------------------------------------------
	}else{
			
			//---------------------------------------------------------------------
		
			}
			//--------------------------------------------------------------------
		}
	}
	}
?>
