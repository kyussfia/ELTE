$kar=read-host "Milyen karakterrel rajzolja?"
$eltolas=read-host "Mennyivel toljuk el?"
$szov=get-content t:\mimtaat\szalap\minta.txt

foreach ($sor in $szov)
{
$i=0
for($k=1;$k -le $eltolas;$k++) {write-host -nonewline " "}
while ($i -lt $sor.length)
{
switch ($sor[$i])
 {
 p {write-host -foregroundcolor red -nonewline $kar;break}
 z {write-host -foregroundcolor green -nonewline $kar;break}
 k {write-host -foregroundcolor blue -nonewline $kar;break}
 s {write-host -foregroundcolor yellow -nonewline $kar;break}
 
 
 }
 $i++
}

write-host

}
