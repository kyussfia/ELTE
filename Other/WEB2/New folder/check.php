<?php
session_start();
$bejel = isset($_SESSION['azonositott']);

header('Location: bead.html');

?>