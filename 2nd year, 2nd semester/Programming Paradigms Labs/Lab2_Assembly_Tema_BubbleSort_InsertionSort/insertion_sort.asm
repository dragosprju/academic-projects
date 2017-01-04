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
    
insertion_sort_start:
    mov bx, offset my_arr
insertion_sort_k_init:
    mov si, 1           ; k = 2
insertion_sort_k:
    mov al, [bx+si]     ; temp = A[k]
    mov di, si          
    dec di              ; i = k-1
    mov ah, [bx+di]     ; al = A[i]
insertion_sort_while:
    cmp di, -1          ; i >= 0
    jle insertion_sort_while_end
    cmp [bx+di], al     ; A[i] > temp
    jle insertion_sort_while_end
    mov ah, [bx+di]
    inc di
    mov [bx+di], ah
    sub di, 2
    jmp insertion_sort_while
insertion_sort_while_end:
    inc di
    mov [bx+di], al
    inc si
    cmp si, 10
    jnz insertion_sort_k  
      
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
  



