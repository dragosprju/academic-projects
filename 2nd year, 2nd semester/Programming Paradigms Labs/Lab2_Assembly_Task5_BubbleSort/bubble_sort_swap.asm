.MODEL Tiny
.DATA
    my_arr db 5,2,8,9,1,7,3,0,4,6

.CODE    
    
bubble_sort_start:
    mov bx, offset my_arr    
    mov di, 9
bubble_sort_i:
    xor si, si
bubble_sort_j:
    mov ah, [bx+si]
    cmp ah, [bx+si+1]
    jle noswap     
    push [bx+si+1]
    push [bx+si]
    call swap
noswap:
    inc si
    cmp si, di
    jnz bubble_sort_j
    xor si, si
    dec di
    cmp di, 0
    jnz bubble_sort_i  
  
print2_start:
    mov bx, offset my_arr
    xor si, si
    mov cx, 10
print2:
    mov dl, [bx+si]
    
    add dl, '0'
    mov ah, 02h
    int 21h
    
    mov dl, ' '
    int 21h
    
    inc si    
    loop print2

exit:
    mov ah, 4Ch
    int 21h
    
swap:
    push bp
    mov bp, sp
    push bx
    
    mov cx,[bp+6]
    and cx, 0FFh
    mov dx,[bp+4]
    and dx, 0FFh
    xchg cx, dx
    
    mov bx,[bp-2]
    mov [bx+si], dl
    mov [bx+si+1], cl
    mov sp,bp
    pop bp
ret
  



