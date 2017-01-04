.MODEL Tiny
.DATA
    my_arr db 5,2,8,9,1,7,3,0,4,6

.CODE
    mov bx, offset my_arr
    xor si, si
    mov cx, 10
print:
    mov dl, [bx+si]
    
    add dl, '0'
    mov ah, 02h
    int 21h
    
    mov dl, ' '
    int 21h
    
    inc si    
    loop print
done:
    mov ah, 4Ch
    int 21h
  



