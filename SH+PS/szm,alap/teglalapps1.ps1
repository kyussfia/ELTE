$sor=5
$oszlop=10

if ($sor -eq $oszlop)
{$sikidom="negyzet"}
else
{$sikidom="teglalap"}

for ($i=1;$i -le $sor;$i++)
{
for($j=1;$j -le $oszlop;$j++)
{
write-host -nonewline "#"
}
write-host 
}
$kerulet=2*($sor+$oszlop)
$terulet=$sor*$oszlop

write-host A téglalap kerülete: $kerulet
write-host A téglalap területe: $terulet
write-host A tábla alakja: $sikidom
 

