REM Please add the two lines for each language with correct folder name, for example tr-TR for Turkish, es-ES for Spanish
if not exist "..\shared\bin\tr-TR\" mkdir "..\shared\bin\tr-TR"
copy %1tr-TR\* ..\shared\bin\tr-TR\
if not exist "..\shared\bin\es-ES\" mkdir "..\shared\bin\es-ES"
copy %1es-ES\* ..\shared\bin\es-ES\
if not exist "..\shared\bin\pt-PT\" mkdir "..\shared\bin\pt-PT"
copy %1pt-PT\* ..\shared\bin\pt-PT\
if not exist "..\shared\bin\it-IT\" mkdir "..\shared\bin\it-IT"
copy %1it-IT\* ..\shared\bin\it-IT\
if not exist "..\shared\bin\fr-CA\" mkdir "..\shared\bin\fr-CA"
copy %1fr-CA\* ..\shared\bin\fr-CA\
if not exist "..\shared\bin\fr-FR\" mkdir "..\shared\bin\fr-FR"
copy %1fr-FR\* ..\shared\bin\fr-FR\
if not exist "..\shared\bin\de-DE\" mkdir "..\shared\bin\de-DE"
copy %1de-DE\* ..\shared\bin\de-DE\
if not exist "..\shared\bin\fi-FI\" mkdir "..\shared\bin\fi-FI"
copy %1fi-FI\* ..\shared\bin\fi-FI\