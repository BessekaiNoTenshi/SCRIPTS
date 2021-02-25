n=int(input("number: "))
def factorization(n):
    p = []
    d = 2
    while d * d <= n:
        while n % d == 0:
            print(d)
            p.append(d)
            n //= d
        d += 1
    if n > 1:
        print(n)
        p.append(n)
        print(p)
    return p
factorization(n)