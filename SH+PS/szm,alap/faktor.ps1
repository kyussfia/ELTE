$szam=read-host "Kérem a számot"
$fakt=1

for ($i=1;$i -le $szam;$i++){
$fakt=$fakt*$i
write-host -nonewline $i
if ($i -eq $szam){write-host -nonewline "="}
else
{write-host -nonewline "*"}
}
write-host $fakt