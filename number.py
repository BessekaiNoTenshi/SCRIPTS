n=int(input("INPUT:"))
i=2
for i in range(2, n+1):
    if n%i==0:
        print (i)
        n=n/i
        continue 
print("target")