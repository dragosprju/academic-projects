.MODEL Tiny
.DATA
    my_arr db 5,2,8,9,1,7,3,0,4,6

.CODE
;    mov bx, offset my_arr
;    xor si, si
;    mov cx, 10
;print:
;    mov dl, [bx+si]
;    
;    add dl, '0'
;    mov ah, 02h
;    int 21h
;    
;    mov dl, ' '
;    int 21h
;    
;    inc si    
;    loop print    
    
bubble_sort_start:
    mov bx, offset my_arr    
    mov dx, 0 
    mov cx, 9
bubble_sort_i:
    xor si, si
    xor di, di
bubble_sort_j:
    mov ah, [bx+si]
    inc di
    mov al, [bx+di]
    cmp ah, [bx+di]
    jle noswap     
    xchg ah, [bx+di]
    xchg ah, [bx+si]
noswap:
    inc si
    loop bubble_sort_j
    inc dx
    mov cx, 9
    sub cx, dx
    cmp dx, 8
    jnz bubble_sort_i  
      
;newline:
;    mov dl, 0Dh ; Trece in capul liniei
;    mov ah, 02h
;    int 21h     ; Scrie caracter
;    mov dl, 0Ah ; Trece la rand nou
;    mov ah, 02h
;    int 21h     ; Scrie caracter
  
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

done:
    mov ah, 4Ch
    int 21h
  



