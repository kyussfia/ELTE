$hanyadik=read-host "Hányadik elemre vagy kíváncsi?"

if($hanyadik -eq 1) {write-host 1}
else
{
$elozo=1
for($i=1;$i -lt $hanyadik;$i++)
{
$gyok=($elozo+(2/$elozo))/2
$elozo=$gyok
}
}
write-host $elozo