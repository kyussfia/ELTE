<?php
include('check.php');
include ('bead.html')

if (!$bejel) {
	header('Location: login.php');
}
else {
	header('Location: bead.html');
}


?>